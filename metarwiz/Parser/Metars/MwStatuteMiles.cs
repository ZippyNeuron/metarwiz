using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwStatuteMiles : BaseMetarItem
    {
        private readonly string _sm;
        private readonly int _distance;

        public MwStatuteMiles(Match match)
        {
            _sm = match.Groups["SM"].Value;
            _ = int.TryParse(match.Groups["DISTANCE"].Value, out _distance);
        }

        public int Distance => _distance;

        public static string Pattern => @"\ (?<DISTANCE>\S+)(?<SM>SM)";

        public override string ToString()
        {
            return String.Concat(
                Distance.ToString(),
                _sm
            );
        }
    }
}
