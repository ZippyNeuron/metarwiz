using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwPressure : BaseMetarItem
    {
        private readonly string _type;
        private readonly int _amount;

        public MwPressure(Match match)
        {
            _type = match.Groups["TYPE"].Value;
            _ = int.TryParse(match.Groups["AMOUNT"].Value, out _amount);
        }

        public decimal HPa => Math.Round(_type.Equals("Q") ? _amount : (_amount / 100m) * MetarConvert.inHgTohPa, 0);
        public decimal InHg => Math.Round(_type.Equals("A") ? (_amount / 100m) : _amount * MetarConvert.hPaToinHg, 2);

        public static string Pattern => @"( )(?<TYPE>Q|A)(?<AMOUNT>\d{4})";

        public override string ToString()
        {
            return $"{_type}{_amount.ToString("D4")}";
        }
    }
}
