using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser
{
    internal class MetarParser : IMetarParser
    {
        private readonly List<MetarParserItem> _items = new();
        private readonly MetarInfo _metarInfo;

        public MetarParser(string metar) : this(metar, null) { }

        public MetarParser(string metar, string tag)
        {
            _metarInfo = new(metar, tag);

            Regex.CacheSize = 128;

            ParseTypes(MetarParserItemType.Metar, _metarInfo.Metar, MetarParserItemTypes.MetarGroup);
            ParseTypes(MetarParserItemType.Remark, _metarInfo.Remarks, MetarParserItemTypes.RemarksGroup);
        }

        public MetarInfo MetarInfo => _metarInfo;

        public IEnumerable<MetarParserItem> Items => _items;

        public IEnumerable<IMetarItem> Parse()
        {
            return _items
                .OrderBy(i => i.Index)
                .Select((value, index) => { value.Item.Position = index; return value.Item; })
                .Cast<IMetarItem>()
                .ToList();
        }

        private void ParseTypes(MetarParserItemType type, string metar, IEnumerable<Type> types)
        {
            string metarCopy = metar;

            foreach (Type t in types)
            {
                string pattern = (string)t
                    .GetProperty("Pattern", BindingFlags.Static | BindingFlags.Public)
                    .GetValue(null, null);

                MatchCollection mc = Regex.Matches(metarCopy, pattern, RegexOptions.None);

                if (mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        try
                        {
                            BaseMetarItem metarItem = MetarParserFactory.Create(t, m);

                            MetarParserItem mpi = new()
                            {
                                Index = _metarInfo.Original.IndexOf(metarItem.ToString()),
                                Item = metarItem,
                                Type = type
                            };

                            _items.Add(mpi);

                            metarCopy = metarCopy.Remove(metarCopy.IndexOf(metarItem.ToString()), mpi.Item.ToString().Length);
                        }
                        catch (MetarException mex) {
                            Debug.WriteLine(mex.Message);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
