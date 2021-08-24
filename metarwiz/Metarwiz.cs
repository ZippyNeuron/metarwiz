using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZippyNeuron.Metarwiz.Abstractions;
using ZippyNeuron.Metarwiz.Factories;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz
{
    public class Metarwiz
    {
        private IDictionary<int, IMetarItem> _metarItems;
        private readonly MetarInfo _metarInfo;

        private Metarwiz(string metar)
        {
            _metarInfo = new MetarInfo(metar);
            _metarItems = ParseParts(_metarInfo.Metar);
        }

        public MetarInfo Metar => _metarInfo;

        public string Remarks => _metarInfo.Remarks;

        private IDictionary<int, IMetarItem> ParseParts(string metar)
        {
            Dictionary<int, IMetarItem> items = new();

            string[] parts = metar.Split(" ");

            for (int i = 0; i < parts.Length; i++)
            {
                items.Add(i, MetarItemFactory.Create(i, parts[i]));
            }

            return items;
        }

        public T Get<T>() where T : IMetarItem
        {
            return _metarItems
                .Where(i => i.Value.GetType() == typeof(T))
                .Select(i => i.Value)
                .Cast<T>()
                .ToList()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetMany<T>() where T : IMetarItem
        {
            return _metarItems
                .Where(i => i.Value.GetType() == typeof(T))
                .Select(i => i.Value)
                .Cast<T>()
                .ToList();
        }

        public static Metarwiz Parse(string metar)
        {
            if (string.IsNullOrEmpty(metar))
                throw new Exception("The report cannot be empty.");

            if (metar.Length < 5 || !metar.StartsWith("METAR"))
                throw new Exception("The report should start with the METAR header.");

            return new Metarwiz(metar);
        }

        public override string ToString()
        {
            StringBuilder builder = new();

            foreach (IMetarItem item in _metarItems.Values)
                builder.Append($"{((item.Position > 0) ? " " : String.Empty)}{item}");

            return $"{builder.ToString().Trim()}{_metarInfo.Terminator}";
        }
    }
}
