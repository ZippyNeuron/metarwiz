using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwWindVariation : BaseMetarItem
    {
        private readonly int _from;
        private readonly int _to;

        public MwWindVariation(Match match)
        {
            _ = int.TryParse(match.Groups["FROM"].Value, out _from);
            _ = int.TryParse(match.Groups["TO"].Value, out _to);
        }

        public int From => _from;

        public int To => _to;

        public static string Pattern => @"\ (?<FROM>\d{3})V(?<TO>\d{3})";

        public override string ToString()
        {
            return String.Concat(
                String.Format("{0:000}", From),
                @"V",
                String.Format("{0:000}", To)
            );
        }
    }
}
