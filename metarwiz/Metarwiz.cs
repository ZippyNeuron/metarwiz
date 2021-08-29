using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZippyNeuron.Metarwiz.Parser;

namespace ZippyNeuron.Metarwiz
{
    public class Metarwiz : IMetarwiz
    {
        private IDictionary<int, IMetarItem> _metarItems;
        private IMetarParser _metarParser;

        public Metarwiz() { }

        public Metarwiz(string metar) => Parse(metar, null);

        public MetarInfo Metar => _metarParser.MetarInfo;

        public T Get<T>() where T : IMetarItem => _metarItems
                .Where(i => i.Value.GetType() == typeof(T))
                .Select(i => i.Value)
                .Cast<T>()
                .ToList()
                .FirstOrDefault();

        public IEnumerable<T> GetMany<T>() where T : IMetarItem => _metarItems
                .Where(i => i.Value.GetType() == typeof(T))
                .Select(i => i.Value)
                .Cast<T>()
                .ToList();

        public void Parse(string metar, string tag)
        {
            if (string.IsNullOrEmpty(metar))
                throw new Exception("The report cannot be empty.");

            if (metar.Length < 5 || !metar.StartsWith("METAR"))
                throw new Exception("The report should start with the METAR header.");

            _metarParser = new MetarParser(metar, tag);
            _metarItems = _metarParser.Parse();
        }

        public static Metarwiz Parse(string metar) => new(metar);

        public override string ToString()
        {
            StringBuilder builder = new();

            foreach (IMetarItem item in _metarItems.Values)
                builder.Append($"{((item.Position > 0) ? " " : String.Empty)}{item}");

            return $"{builder.ToString().Trim()}{_metarParser.MetarInfo.Terminator}";
        }
    }
}
