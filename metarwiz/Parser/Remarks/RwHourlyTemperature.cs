using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwHourlyTemperature : BaseMetarItem
    {
        private readonly string _t;
        private readonly decimal _units = 0.10m;
        private readonly int _minust;
        private readonly int _temperature;
        private readonly int _minusdp;
        private readonly int _dewpoint;

        public RwHourlyTemperature(Match match)
        {
            _t = match.Groups["T"].Value;
            _ = int.TryParse(match.Groups["MT"].Value, out _minust);
            _ = int.TryParse(match.Groups["TEMPERATURE"].Value, out _temperature);
            _ = int.TryParse(match.Groups["MDP"].Value, out _minusdp);
            _ = int.TryParse(match.Groups["DEWPOINT"].Value, out _dewpoint);
        }

        public decimal Celsius => ((_minust == 1) ? _temperature * -1 : _temperature) * _units;
        public decimal DewPoint => ((_minusdp == 1) ? _dewpoint * -1 : _dewpoint) * _units;

        public static string Pattern => @"( )(?<T>T)(?<MT>\d{1})(?<TEMPERATURE>\d{3})(?<MDP>\d{1})(?<DEWPOINT>\d{3})";

        public override string ToString()
        {
            return $"{_t}{_minust.ToString("D1")}{_temperature.ToString("D3")}{_minusdp.ToString("D1")}{_dewpoint.ToString("D3")}";
        }
    }
}
