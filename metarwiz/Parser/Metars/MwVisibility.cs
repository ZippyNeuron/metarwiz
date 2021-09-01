using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwVisibility : BaseMetarItem
    {
        private readonly int _distance;

        public MwVisibility(Match match)
        {
            _ = int.TryParse(match.Groups["DISTANCE"].Value, out _distance);
        }

        public int Distance => _distance;

        public bool IsMoreThanTenKilometres => _distance >= 9999;

        public static string Pattern => @"\ (?<DISTANCE>\d{4})";

        public override string ToString()
        {
            return String.Format("{0:0000}", Distance);
        }
    }
}
