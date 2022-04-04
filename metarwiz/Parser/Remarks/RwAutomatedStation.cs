using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwAutomatedStation : BaseMetarItem
    {
        private readonly string _ao;
        private readonly int _precipitationDiscriminator;

        public RwAutomatedStation(Match match)
        {
            _ao = match.Groups["AO"].Value;
            _ = int.TryParse(match.Groups["PRECIPITATIONDISCRIMINATOR"].Value, out _precipitationDiscriminator);
        }

        public bool HasPrecipitationDiscriminator => _precipitationDiscriminator == 2;

        public static string Pattern => @"( )(?<AO>AO)(?<PRECIPITATIONDISCRIMINATOR>\d{1})";

        public override string ToString()
        {
            return $"{_ao}{_precipitationDiscriminator.ToString("D1")}";
        }
    }
}
