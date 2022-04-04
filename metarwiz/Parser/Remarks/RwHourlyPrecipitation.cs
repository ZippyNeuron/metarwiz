using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwHourlyPrecipitation : BaseMetarItem
    {
        private readonly string _p;
        private readonly decimal _units = 0.01m;
        private readonly int _amount;

        public RwHourlyPrecipitation(Match match)
        {
            _p = match.Groups["P"].Value;
            _ = int.TryParse(match.Groups["AMOUNT"].Value, out _amount);
        }

        public bool IsTrace => _amount == 0;
        public decimal Inches => _amount * _units;

        public static string Pattern => @"( )(?<P>P)(?<AMOUNT>\d{4})";

        public override string ToString()
        {
            return $"{_p}{_amount.ToString("D4")}";
        }
    }
}
