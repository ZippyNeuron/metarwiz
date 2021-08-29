using System;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwPressureTendency : RwMetarItem
    {
        private readonly string _five;
        private readonly PressureTendencyType _type;
        private readonly int _pressure;

        public RwPressureTendency(int position, string value) : base(position, value, Pattern)
        {
            _five = Groups["5"].Value;
            _ = Enum.TryParse(Groups["A"].Value, out _type);
            _ = int.TryParse(Groups["PRESSURE"].Value, out _pressure);
        }

        public PressureTendencyType Type => _type;

        public decimal HPa => Math.Round((_pressure / 10) + ((_pressure < 500) ? 1000m : 900m), 0);

        public decimal InHg => Math.Round(HPa * MetarConvert.hPaToinHg, 2);

        public static string Pattern => @"^(?<5>5)(?<A>\d{1})(?<PRESSURE>\d{3})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _five,
                String.Format("{0:0}", (int)_type),
                String.Format("{0:000}", _pressure)
            );
        }
    }
}
