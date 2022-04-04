using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwSeaLevelPressure : BaseMetarItem
    {
        private readonly string _slp;
        private readonly int _pressure;

        public RwSeaLevelPressure(Match match)
        {
            _slp = match.Groups["SLP"].Value;
            _ = int.TryParse(match.Groups["PRESSURE"].Value, out _pressure);
        }

        public decimal HPa => Math.Round((_pressure / 10) + ((_pressure < 500) ? 1000m : 900m), 0);

        public decimal InHg => Math.Round(HPa * MetarConvert.hPaToinHg, 2);

        public static string Pattern => @"( )(?<SLP>SLP)(?<PRESSURE>\d{3})";

        public override string ToString()
        {
            return $"{_slp}{_pressure.ToString("D3")}";
        }
    }
}
