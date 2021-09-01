using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwRainEnded : BaseMetarItem
    {
        private readonly string _rae;
        private readonly string _minutepastthehour;
        private readonly string _exacttime;
        private readonly TimeSpan? _time;
        private readonly int? _minutes;

        public RwRainEnded(Match match)
        {
            _rae = match.Groups["RAE"].Value;
            _minutepastthehour = match.Groups["MINUTEPASTTHEHOUR"].Value;
            _exacttime = match.Groups["EXACTTIME"].Value;
            _time = !String.IsNullOrEmpty(_exacttime) ? TimeSpan.ParseExact(_exacttime, "hhmm", null) : null;
            _minutes = !String.IsNullOrEmpty(_minutepastthehour) ? int.Parse(_minutepastthehour) : null;
        }

        public TimeSpan? Time => _time;

        public int? Minutes => _minutes;

        public static string Pattern => @"\ (?<RAE>RAE)((?<EXACTTIME>\d{4})|(?<MINUTEPASTTHEHOUR>\d{2}))";

        public override string ToString()
        {
            return String.Concat(
                _rae,
                (_time != null) ? _time?.ToString("hhmm") : String.Format("{0:00}", _minutes)
            );
        }
    }
}
