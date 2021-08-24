using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwPressure : MvMetarItem
    {
        private const decimal inHgTohPa = 33.86389m;
        private const decimal hPaToinHg = 00.02953m;
        private readonly string _type;
        private readonly decimal _amount;

        public MwPressure(int position, string value) : base(position, value, Pattern)
        {
            _type = Groups["TYPE"].Value;
            _ = decimal.TryParse(Groups["AMOUNT"].Value, out _amount);
        }

        /* hPa (hectopascal) */
        public decimal hPa => Math.Round((_type == "Q") ? _amount : (_amount / 100) * inHgTohPa, 0);

        /* inHg (inch of mercury) */
        public decimal inHg => Math.Round((_type == "A") ? (_amount / 100) : _amount * hPaToinHg, 2);

        public static string Pattern => @"^(?<TYPE>Q|A)(?<AMOUNT>\d+)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(_type, String.Format("{0:0000}", _amount));
        }
    }
}
