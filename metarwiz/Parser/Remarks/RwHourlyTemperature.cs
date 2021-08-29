using System;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwHourlyTemperature : RwMetarItem
    {
        private readonly string _t;
        private readonly decimal _units = 0.10m;
        private readonly int _minust;
        private readonly int _temperature;
        private readonly int _minusdp;
        private readonly int _dewpoint;

        public RwHourlyTemperature(int position, string value) : base(position, value, Pattern)
        {
            _t = Groups["T"].Value;
            _ = int.TryParse(Groups["MT"].Value, out _minust);
            _ = int.TryParse(Groups["TEMPERATURE"].Value, out _temperature);
            _ = int.TryParse(Groups["MDP"].Value, out _minusdp);
            _ = int.TryParse(Groups["DEWPOINT"].Value, out _dewpoint);
        }

        public decimal Celsius => ((_minust == 1) ? _temperature * -1 : _temperature) * _units;

        public decimal DewPoint => ((_minusdp == 1) ? _dewpoint * -1 : _dewpoint) * _units;

        public static string Pattern => @"^(?<T>T)(?<MT>\d{1})(?<TEMPERATURE>\d{3})(?<MDP>\d{1})(?<DEWPOINT>\d{3})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _t,
                String.Format("{0:0}", _minust),
                String.Format("{0:000}", _temperature),
                String.Format("{0:0}", _minusdp),
                String.Format("{0:000}", _dewpoint)
            );
        }
    }
}
