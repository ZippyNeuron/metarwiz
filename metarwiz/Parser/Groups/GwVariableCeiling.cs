using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Groups
{
    public class GwVariableCeiling : BaseMetarItem
    {
        private const int _multiplier = 100;
        private readonly string _cig;
        private readonly int _from;
        private readonly int _to;

        public GwVariableCeiling(Match match)
        {
            _cig = match.Groups["CIG"].Value;
            _ = int.TryParse(match.Groups["FROM"].Value, out _from);
            _ = int.TryParse(match.Groups["TO"].Value, out _to);
        }

        public int From => _from * _multiplier;

        public int To => _to * _multiplier;

        public static string Pattern => @"\ (?<CIG>CIG)\ (?<FROM>\d{3})V(?<TO>\d{3})";

        public override string ToString()
        {
            return String.Concat(
                _cig,
                " ",
                String.Format("{0:000}", _from),
                @"V",
                String.Format("{0:000}", _to)
            );
        }
    }
}
