using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwTimeOfObservation : MwMetarItem
    {
        private readonly int _day;
        private readonly int _hour;
        private readonly int _minute;
        private readonly DateTime _date;
        
        public MwTimeOfObservation(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["DAY"].Value, out _day);
            _ = int.TryParse(Groups["HOUR"].Value, out _hour);
            _ = int.TryParse(Groups["MINUTE"].Value, out _minute);

            _date = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                _day,
                _hour,
                _minute,
                0
                );
        }

        public int Day => _day;

        public int Hour => _hour;

        public int Minute => _minute;

        [Obsolete("This property will be removed with next release")]
        public DateTime Date => _date;

        public static string Pattern => @"^(?<DAY>\d{2})(?<HOUR>\d{2})(?<MINUTE>\d{2})([Z])$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                String.Format("{0:00}", Day),
                String.Format("{0:00}", Hour),
                String.Format("{0:00}", Minute),
                "Z"
            );
        }
    }
}
