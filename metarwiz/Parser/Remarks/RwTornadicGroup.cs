using System;
using System.Linq;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwTornadicGroup : BaseMetarItem
    {
        private readonly TornadicType _activity;
        private readonly string _be;
        private readonly string _exacttime;
        private readonly string _minutepastthehour;
        private readonly TimeSpan? _time;
        private readonly int? _minutes;
        private readonly int _distance;
        private readonly string _movement;

        public RwTornadicGroup(Match match)
        {
            _ = Enum.TryParse<TornadicType>(match.Groups["ACTIVITY"].Value.Replace(" ", "_"), out _activity);
            _be = match.Groups["BE"].Value;
            _exacttime = match.Groups["EXACTTIME"].Value;
            _minutepastthehour = match.Groups["MINUTEPASTTHEHOUR"].Value;
            _time = !String.IsNullOrEmpty(_exacttime) ? TimeSpan.ParseExact(_exacttime, "hhmm", null) : null;
            _minutes = !String.IsNullOrEmpty(_minutepastthehour) ? int.Parse(_minutepastthehour) : null;
            _ = int.TryParse(match.Groups["SM"].Value, out _distance);
            _movement = match.Groups["MOVEMENT"].Value;
        }

        public TimeSpan? Time => _time;
        public int? Minutes => _minutes;
        public TornadicType Activity => _activity;
        public string ActivityDescription => _activity.GetDescription();
        public bool HasEnded => _be == "E";
        public int Distance => _distance;
        public string Movement => _movement;

        public static string Pattern
        {
            get
            {
                string[] names = Enum
                    .GetNames<TornadicType>()
                    .Select(i => i.Replace("_", " "))
                    .ToArray();

                string clouds = String
                    .Join("|", names);

                return String.Concat(@$"( )(?<ACTIVITY>{clouds})", @"\ (?<BE>B|E)((?<EXACTTIME>\d{4})|(?<MINUTEPASTTHEHOUR>\d{2}))\ (?<SM>\d+)\ (?<MOVEMENT>\S+)");
            }
        }

        public override string ToString()
        {
            return
                String.Concat(
                    Enum.GetName(_activity).Replace("_", " "),
                    " ",
                    _be,
                    (_time != null) ? _time?.ToString("hhmm") : String.Format("{0:00}", _minutes),
                    " ",
                    _distance.ToString(),
                    " ",
                    _movement
                );
        }
    }
}
