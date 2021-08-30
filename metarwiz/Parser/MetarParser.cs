using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser
{
    internal class MetarParser : IMetarParser
    {
        public MetarParser(string metar) : this(metar, null) { }

        public MetarParser(string metar, string tag)
        {
            MetarInfo = new(metar, tag);

            string[] items = Regex
                .Split(metar, @"(CIG \d{3}V\d{3}(\ |))|(\S+(\ |))", RegexOptions.None)
                .Where((item) => !string.IsNullOrEmpty(item.Trim()))
                .Select(item => item.Trim())
                .ToArray();

            int rmk = items
                .Select((item, index) =>
                {
                    return (item == "RMK") ? index : -1;
                })
                .Where(i => i > 0)
                .FirstOrDefault();

            Items = items
                .Select(
                    (item, index) =>
                    {
                        return new MetarParserItem(index, item, (rmk == 0 || index < rmk) ? MetarParserItemType.Metar : MetarParserItemType.Remark);
                    }
            );
        }

        public MetarInfo MetarInfo { get; }

        public IEnumerable<MetarParserItem> Items { get; }

        public IDictionary<int, IMetarItem> Parse()
        {
            IDictionary<int, IMetarItem> items = new Dictionary<int, IMetarItem>();

            MetarParserFactory factory = new();

            foreach (MetarParserItem item in Items)
            {
                IMetarItem i = factory.Create(item);

                if (i != null)
                    items.Add(i.Position, i);
            }

            return items;
        }
    }
}
