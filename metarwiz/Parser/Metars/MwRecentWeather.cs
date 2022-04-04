using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwRecentWeather : BaseMetarItem
    {
        private readonly string _recent;

        public MwRecentWeather(Match match)
        {
            _recent = match.Groups["RECENTWEATHER"].Value;
        }

        public RecentWeatherType Kind => (!String.IsNullOrEmpty(_recent)) ? Enum.Parse<RecentWeatherType>(_recent) : RecentWeatherType.Unspecified;
        public string KindDescription => Kind.GetDescription();

        public static string Pattern
        {
            get
            {
                string recents = String
                    .Join("|", Enum.GetNames<RecentWeatherType>());

                return @$"( )(?<RECENTWEATHER>{recents})";
            }
        }

        public override string ToString()
        {
            return Enum.GetName<RecentWeatherType>(Kind);
        }
    }
}
