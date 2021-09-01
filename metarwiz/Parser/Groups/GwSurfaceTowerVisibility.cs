using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Groups
{
    public class GwSurfaceTowerVisibility : BaseMetarItem
    {
        private readonly string _type;
        private readonly string _vis;
        private readonly decimal _distance;
        private readonly decimal _pt1;
        private readonly decimal _pt2;

        public GwSurfaceTowerVisibility(Match match)
        {
            _type = match.Groups["TYPE"].Value;
            _vis = match.Groups["VIS"].Value;
            _ = decimal.TryParse(match.Groups["VALUE"].Value, out _distance);
            _ = decimal.TryParse(match.Groups["PT1"].Value, out _pt1);
            _ = decimal.TryParse(match.Groups["PT2"].Value, out _pt2);
        }

        public decimal Distance => Math.Round(_distance + ((_pt2 > 0) ? (_pt1 / _pt2) : 0m), 2);

        public static string Pattern => @"\ (?<TYPE>SFC|TWR)\ (?<VIS>VIS)\ (?<VALUE>\d{1})(\ (?<PT1>\d+)\/(?<PT2>\d+))?";

        public override string ToString()
        {
            return String.Concat(
                _type,
                " ",
                _vis,
                " ",
                String.Format("{0:0}", _distance),
                (_pt1 > 0 && _pt2 > 0) ? $" {_pt1}/{_pt2}" : String.Empty
            );
        }
    }
}
