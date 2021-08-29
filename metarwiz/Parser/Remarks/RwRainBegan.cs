using System;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwRainBegan : RwMetarItem
    {
        private readonly string _rab;
        private readonly string _minutepastthehour;
        private readonly string _exacttime;
        private readonly TimeSpan? _time;
        private readonly int? _minutes;

        public RwRainBegan(int position, string value) : base(position, value, Pattern)
        {
            _rab = Groups["RAB"].Value;
            _minutepastthehour = Groups["MINUTEPASTTHEHOUR"].Value;
            _exacttime = Groups["EXACTTIME"].Value;
            _time = !String.IsNullOrEmpty(_exacttime) ? TimeSpan.ParseExact(_exacttime, "hhmm", null) : null;
            _minutes = !String.IsNullOrEmpty(_minutepastthehour) ? int.Parse(_minutepastthehour) : null;
        }

        public TimeSpan? Time => _time;

        public int? Minutes => _minutes;

        public static string Pattern => @"^(?<RAB>RAB)((?<MINUTEPASTTHEHOUR>\d{2})|(?<EXACTTIME>\d{4}))$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _rab,
                (_time != null) ? _time?.ToString("hhmm") : String.Format("{0:00}", _minutes)
            );
        }
    }
}
