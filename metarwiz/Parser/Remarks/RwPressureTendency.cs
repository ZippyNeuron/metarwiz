using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwPressureTendency : BaseMetarItem
    {
        private readonly string _prefix;
        private readonly PressureTendencyType _type;
        private readonly int _pressure;

        public RwPressureTendency(Match match)
        {
            _prefix = match.Groups["5"].Value;
            _ = Enum.TryParse(match.Groups["A"].Value, out _type);
            _ = int.TryParse(match.Groups["PRESSURE"].Value, out _pressure);
        }

        public PressureTendencyType Type => _type;

        public string TypeDescription => Type.GetDescription();

        public decimal HPa => Math.Round((_pressure / 10) + ((_pressure < 500) ? 1000m : 900m), 0);

        public decimal InHg => Math.Round(HPa * MetarConvert.hPaToinHg, 2);

        public static string Pattern => @"\ (?<5>5)(?<A>\d{1})(?<PRESSURE>\d{3})";

        public override string ToString()
        {
            return $"{_prefix}{String.Format("{0:0}", (int)_type)}{_pressure.ToString("D3")}";
        }
    }
}
