using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwWindShiftGroup : BaseMetarItem
    {
        private readonly string _fropa;
        private readonly string _exacttime;
        private readonly string _minutepastthehour;
        private readonly TimeSpan? _time;
        private readonly int? _minutes;
        private readonly string _wshft;

        public RwWindShiftGroup(Match match)
        {
            _wshft = match.Groups["WSHFT"].Value;
            _fropa = match.Groups["FROPA"].Value;
            _exacttime = match.Groups["EXACTTIME"].Value;
            _minutepastthehour = match.Groups["MINUTEPASTTHEHOUR"].Value;
            _time = !String.IsNullOrEmpty(_exacttime) ? TimeSpan.ParseExact(_exacttime, "hhmm", null) : null;
            _minutes = !String.IsNullOrEmpty(_minutepastthehour) ? int.Parse(_minutepastthehour) : null;
        }

        public TimeSpan? Time => _time;
        public int? Minutes => _minutes;
        public bool IsFrontalPassage => !String.IsNullOrEmpty(_fropa);

        public static string Pattern
        {
            get
            {
                return @"( )(?<WSHFT>WSHFT)\ ((?<EXACTTIME>\d{4})|(?<MINUTEPASTTHEHOUR>\d{2}))(\ (?<FROPA>FROPA))?";
            }
        }

        public override string ToString()
        {
            return
                String.Concat(
                    _wshft,
                    " ",
                    (_time != null) ? _time?.ToString("hhmm") : _minutes?.ToString("D2"),
                    (!String.IsNullOrEmpty(_fropa)) ? $" {_fropa}" : String.Empty
                );
        }
    }
}
