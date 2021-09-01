using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwCavok : BaseMetarItem
    {
        private readonly string _cavok;

        public MwCavok(Match match)
        {
            _cavok = match.Groups["CAVOK"].Value;
        }

        public static string Pattern => @"\ (?<CAVOK>CAVOK)";

        public override string ToString()
        {
            return _cavok;
        }
    }
}
