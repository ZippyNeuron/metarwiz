using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwRemarks : BaseMetarItem
    {
        private readonly string _rmk;

        public RwRemarks(Match match)
        {
            _rmk = match.Groups["RMK"].Value;
        }

        public static string Pattern => @"(?<RMK>RMK)";

        public override string ToString()
        {
            return _rmk;
        }
    }
}
