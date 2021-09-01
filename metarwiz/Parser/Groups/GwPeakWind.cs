using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Groups
{
    public class GwPeakWind : BaseMetarItem
    {
        private readonly string _exacttime;
        private readonly string _minutepastthehour;
        private readonly TimeSpan? _time;
        private readonly int? _minutes;
        private readonly int _speed;
        private readonly int _direction;
        private readonly string _pkwnd;

        public GwPeakWind(Match match)
        {
            _pkwnd = match.Groups["PKWND"].Value;
            _exacttime = match.Groups["EXACTTIME"].Value;
            _minutepastthehour = match.Groups["MINUTEPASTTHEHOUR"].Value;
            _time = !String.IsNullOrEmpty(_exacttime) ? TimeSpan.ParseExact(_exacttime, "hhmm", null) : null;
            _minutes = !String.IsNullOrEmpty(_minutepastthehour) ? int.Parse(_minutepastthehour) : null;
            _ = int.TryParse(match.Groups["SPEED"].Value, out _speed);
            _ = int.TryParse(match.Groups["DIRECTION"].Value, out _direction);
        }

        public TimeSpan? Time => _time;

        public int? Minutes => _minutes;

        public int Speed => _speed;

        public int Direction => _direction;

        public static string Pattern => GetPattern();

        private static string GetPattern()
        {
            return @"\ (?<PKWND>PK\ WND)\ (?<DIRECTION>\d{3})(?<SPEED>\d{2})\/((?<EXACTTIME>\d{4})|(?<MINUTEPASTTHEHOUR>\d{2}))";
        }
        
        public override string ToString()
        {
            return
                String.Concat(
                    _pkwnd,
                    " ",
                    String.Format("{0:000}", _direction),
                    String.Format("{0:00}", _speed),
                    "/",
                    (_time != null) ? _time?.ToString("hhmm") : String.Format("{0:00}", _minutes)
                );
        }
    }
}
