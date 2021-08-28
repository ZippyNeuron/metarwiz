using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZippyNeuron.Metarwiz.Abstractions;
using ZippyNeuron.Metarwiz.Factories;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz
{
    public class Metarwiz : IMetarwiz
    {
        private IDictionary<int, IMetarItem> _metarItems;
        private MetarInfo _metarInfo;

        public Metarwiz() { }

        public Metarwiz(string metar) => Parse(metar, null);

        public MetarInfo Metar => _metarInfo;

        [Obsolete("This property will be removed with next release, please use Metar.Remarks or Metar.HasRemarks instead")]
        public string Remarks => _metarInfo.Remarks;

        private IDictionary<int, IMetarItem> ParseMetar(MetarInfo metarInfo)
        {
            IDictionary<int, IMetarItem> items = new Dictionary<int, IMetarItem>();
            IMetarItemFactory factory;

            int position = 0;

            foreach (string part in metarInfo.Metar.Split(" "))
            {
                position++;
                factory = new MetarItemFactory();

                IMetarItem item = factory.Create(position, part);

                if (item != null)
                    items.Add(position, item);
            }

            foreach (string part in metarInfo.Remarks.Split(" "))
            {
                position++;
                factory = new MetarRemarksFactory();

                IMetarItem item = factory.Create(position, part);

                if (item != null)
                    items.Add(position, item);
            }

            return items;
        }

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

            _metarInfo = new(metar, tag);
            _metarItems = ParseMetar(_metarInfo);
        }

        public static Metarwiz Parse(string metar) => new(metar);

        public override string ToString()
        {
            StringBuilder builder = new();

            foreach (IMetarItem item in _metarItems.Values)
                builder.Append($"{((item.Position > 1) ? " " : String.Empty)}{item}");

            return $"{builder.ToString().Trim()}{_metarInfo.Terminator}";
        }
    }
}
