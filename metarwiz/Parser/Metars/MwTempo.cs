using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwTempo : BaseMetarItem
    {
        private readonly string _tempo;

        public MwTempo(Match match)
        {
            _tempo = match.Groups["TEMPO"].Value;
        }

        public static string Pattern => @"\ (?<TEMPO>TEMPO)";

        public override string ToString()
        {
            return _tempo;
        }
    }
}
