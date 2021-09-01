using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwLocation : BaseMetarItem
    {
        private readonly string _icao;

        public MwLocation(Match match)
        {
            _icao = match.Groups["ICAO"].Value;
        }

        public string ICAO => _icao;

        public static string Pattern => @"^METAR\ (?<ICAO>[A-Z]{4})";

        public override string ToString()
        {
            return _icao;
        }
    }
}
