using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwNoSig : BaseMetarItem
    {
        private readonly string _nosig;

        public MwNoSig(Match match)
        {
            _nosig = match.Groups["NOSIG"].Value;
        }

        public static string Pattern => @"( )(?<NOSIG>NOSIG)";

        public override string ToString()
        {
            return _nosig;
        }
    }
}
