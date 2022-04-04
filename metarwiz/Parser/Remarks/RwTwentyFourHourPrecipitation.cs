using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwTwentyFourHourPrecipitation : BaseMetarItem
    {
        private readonly string _prefix;
        private readonly decimal _units = 0.01m;
        private readonly int _amount;

        public RwTwentyFourHourPrecipitation(Match match)
        {
            _prefix = match.Groups["7"].Value;
            _ = int.TryParse(match.Groups["AMOUNT"].Value, out _amount);
        }

        public bool IsTrace => _amount == 0;
        public decimal Inches => _amount * _units;

        public static string Pattern => @"( )(?<7>7)(?<AMOUNT>\d{4})";

        public override string ToString()
        {
            return $"{_prefix}{_amount.ToString("D4")}";
        }
    }
}
