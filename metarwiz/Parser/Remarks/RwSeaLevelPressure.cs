using System;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwSeaLevelPressure : RwMetarItem
    {
        private readonly string _slp;
        private readonly int _pressure;

        public RwSeaLevelPressure(int position, string value) : base(position, value, Pattern)
        {
            _slp = Groups["SLP"].Value;
            _ = int.TryParse(Groups["PRESSURE"].Value, out _pressure);
        }

        public decimal HPa => Math.Round((_pressure / 10) + ((_pressure < 500) ? 1000m : 900m), 0);

        public decimal InHg => Math.Round(HPa * MetarConvert.hPaToinHg, 2);

        public static string Pattern => @"^(?<SLP>SLP)(?<PRESSURE>\d{3})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _slp, 
                String.Format("{0:000}", _pressure)
            );
        }
    }
}
