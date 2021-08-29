using System;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Parser.Metar
{
    public class MwPressure : MwMetarItem
    {
        private readonly string _type;
        private readonly decimal _amount;

        public MwPressure(int position, string value) : base(position, value, Pattern)
        {
            _type = Groups["TYPE"].Value;
            _ = decimal.TryParse(Groups["AMOUNT"].Value, out _amount);
        }

        public decimal HPa => Math.Round((_type == "Q") ? _amount : (_amount / 100) * MetarConvert.inHgTohPa, 0);

        public decimal InHg => Math.Round((_type == "A") ? (_amount / 100) : _amount * MetarConvert.hPaToinHg, 2);

        public static string Pattern => @"^(?<TYPE>Q|A)(?<AMOUNT>\d+)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(_type, String.Format("{0:0000}", _amount));
        }
    }
}
