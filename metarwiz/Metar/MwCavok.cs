using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwCavok : MwMetarItem
    {
        private readonly string _cavok;

        public MwCavok(int position, string value) : base(position, value, Pattern)
        {
            _cavok = Groups["CAVOK"].Value;
        }

        [Obsolete("This property will be removed with next release")]
        public bool IsCavok => _cavok == "CAVOK";

        public static string Pattern => @"^(?<CAVOK>CAVOK)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return _cavok;
        }
    }
}
