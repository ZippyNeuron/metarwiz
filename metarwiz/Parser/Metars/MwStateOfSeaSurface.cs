using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwStateOfSeaSurface : BaseMetarItem
    {
        private readonly string _prefix;
        private readonly string _tempSign;
        private readonly int _surfaceTemp;
        private readonly int _seaState;
        private readonly string _separator;
        private readonly string _s;

        public MwStateOfSeaSurface(Match match)
        {
            _prefix = match.Groups["PREFIX"].Value;
            _tempSign = match.Groups["SURFACETEMPSIGN"].Value;
            _ = int.TryParse(match.Groups["SURFACETEMP"].Value, out _surfaceTemp);
            _ = int.TryParse(match.Groups["SEASTATE"].Value, out _seaState);
            _s = match.Groups["S"].Value;
            _separator = match.Groups["SEPARATOR"].Value;
        }

        public int Celsius => (_tempSign == "M") ? _surfaceTemp * -1 : _surfaceTemp;
        public int SeaState => _seaState;

        public static string Pattern => @"( )(?<PREFIX>W)(?<SURFACETEMPSIGN>M)?(?<SURFACETEMP>\d+)(?<SEPARATOR>\/)(?<S>S)(?<SEASTATE>\d+)";

        public override string ToString()
        {
            return $"{_prefix}{_tempSign}{Celsius.ToString()}{_separator}{_s}{_seaState.ToString()}";
        }
    }
}
