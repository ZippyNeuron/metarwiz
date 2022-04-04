using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwTimeOfObservation : BaseMetarItem
    {
        private readonly int _day;
        private readonly int _hour;
        private readonly int _minute;
        private readonly string _timezone;
        
        public MwTimeOfObservation(Match match)
        {
            _ = int.TryParse(match.Groups["DAY"].Value, out _day);
            _ = int.TryParse(match.Groups["HOUR"].Value, out _hour);
            _ = int.TryParse(match.Groups["MINUTE"].Value, out _minute);
            _timezone = match.Groups["TIMEZONE"].Value;
        }

        public int Day => _day;

        public int Hour => _hour;

        public int Minute => _minute;

        public static string Pattern => @"( )(?<DAY>\d{2})(?<HOUR>\d{2})(?<MINUTE>\d{2})(?<TIMEZONE>[Z])";

        public override string ToString()
        {
            return $"{_day.ToString("D2")}{_hour.ToString("D2")}{_minute.ToString("D2")}{_timezone}";
        }
    }
}
