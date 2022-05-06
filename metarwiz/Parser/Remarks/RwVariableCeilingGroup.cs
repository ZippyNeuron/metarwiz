using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwVariableCeilingGroup : BaseMetarItem
    {
        private const int _multiplier = 100;
        private readonly string _cig;
        private readonly int _from;
        private readonly int _to;
        private readonly string _v;

        public RwVariableCeilingGroup(Match match)
        {
            _cig = match.Groups["CIG"].Value;
            _ = int.TryParse(match.Groups["FROM"].Value, out _from);
            _ = int.TryParse(match.Groups["TO"].Value, out _to);
            _v = match.Groups["V"].Value;
        }

        public int From => _from * _multiplier;
        public int To => _to * _multiplier;

        public static string Pattern => @"( )(?<CIG>CIG)\ (?<FROM>\d{3})(?<V>V)(?<TO>\d{3})";

        public override string ToString()
        {
            return $"{_cig} {_from.ToString("D3")}{_v}{_to.ToString("D3")}";
        }
    }
}
