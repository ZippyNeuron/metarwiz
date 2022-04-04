using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwSixHourMaxTemperature : BaseMetarItem
    {
        private readonly string _prefix;
        private readonly decimal _units = 0.10m;
        private readonly int _ma;
        private readonly int _amount;

        public RwSixHourMaxTemperature(Match match)
        {
            _prefix = match.Groups["1"].Value;
            _ = int.TryParse(match.Groups["MA"].Value, out _ma);
            _ = int.TryParse(match.Groups["AMOUNT"].Value, out _amount);
        }

        public decimal Celsius => ((_ma == 1) ? _amount * -1 : _amount) * _units;

        public static string Pattern => @"( )(?<1>1)(?<MA>\d{1})(?<AMOUNT>\d{3})";

        public override string ToString()
        {
            return $"{_prefix}{_ma.ToString("D1")}{_amount.ToString("D3")}";
        }
    }
}
