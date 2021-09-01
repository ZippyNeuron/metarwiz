using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwRainBegan : BaseMetarItem
    {
        private readonly string _rab;
        private readonly string _minutepastthehour;
        private readonly string _exacttime;
        private readonly TimeSpan? _time;
        private readonly int? _minutes;

        public RwRainBegan(Match match)
        {
            _rab = match.Groups["RAB"].Value;
            _minutepastthehour = match.Groups["MINUTEPASTTHEHOUR"].Value;
            _exacttime = match.Groups["EXACTTIME"].Value;
            _time = !String.IsNullOrEmpty(_exacttime) ? TimeSpan.ParseExact(_exacttime, "hhmm", null) : null;
            _minutes = !String.IsNullOrEmpty(_minutepastthehour) ? int.Parse(_minutepastthehour) : null;
        }

        public TimeSpan? Time => _time;

        public int? Minutes => _minutes;

        public static string Pattern => @"\ (?<RAB>RAB)((?<EXACTTIME>\d{4})|(?<MINUTEPASTTHEHOUR>\d{2}))";

        public override string ToString()
        {
            return String.Concat(
                _rab,
                (_time != null) ? _time?.ToString("hhmm") : String.Format("{0:00}", _minutes)
            );
        }
    }
}
