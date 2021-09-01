using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwTimeOfObservation : BaseMetarItem
    {
        private readonly int _day;
        private readonly int _hour;
        private readonly int _minute;
        
        public MwTimeOfObservation(Match match)
        {
            _ = int.TryParse(match.Groups["DAY"].Value, out _day);
            _ = int.TryParse(match.Groups["HOUR"].Value, out _hour);
            _ = int.TryParse(match.Groups["MINUTE"].Value, out _minute);
        }

        public int Day => _day;

        public int Hour => _hour;

        public int Minute => _minute;

        public static string Pattern => @"\ (?<DAY>\d{2})(?<HOUR>\d{2})(?<MINUTE>\d{2})([Z])";

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
