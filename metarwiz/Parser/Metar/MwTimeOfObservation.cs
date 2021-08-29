using System;

namespace ZippyNeuron.Metarwiz.Parser.Metar
{
    public class MwTimeOfObservation : MwMetarItem
    {
        private readonly int _day;
        private readonly int _hour;
        private readonly int _minute;
        
        public MwTimeOfObservation(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["DAY"].Value, out _day);
            _ = int.TryParse(Groups["HOUR"].Value, out _hour);
            _ = int.TryParse(Groups["MINUTE"].Value, out _minute);
        }

        public int Day => _day;

        public int Hour => _hour;

        public int Minute => _minute;

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
