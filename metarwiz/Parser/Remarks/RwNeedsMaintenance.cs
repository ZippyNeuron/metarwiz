using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwNeedsMaintenance : BaseMetarItem
    {
        private readonly string _maintenance;

        public RwNeedsMaintenance(Match match)
        {
            _maintenance = match.Groups["MAINTENANCE"].Value;
        }

        public static string Pattern => @"\ (?<MAINTENANCE>\$)";

        public override string ToString()
        {
            return _maintenance;
        }
    }
}
