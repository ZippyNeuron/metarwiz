using System;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwRecentWeather : MwMetarItem
    {
        private readonly string _recent;

        public MwRecentWeather(int position, string value) : base(position, value, Pattern)
        {
            _recent = Groups["RECENTWEATHER"].Value;
        }

        public RecentWeatherType Kind => (!String.IsNullOrEmpty(_recent)) ? Enum.Parse<RecentWeatherType>(_recent) : RecentWeatherType.Unspecified;

        public string KindDescription => Kind.GetDescription();

        private static string GetPattern()
        {
            string recents = String
                .Join("|", Enum.GetNames<RecentWeatherType>());

            return $@"^(?<RECENTWEATHER>{recents})$";
        }

        public static string Pattern => GetPattern();

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return Enum.GetName<RecentWeatherType>(Kind);
        }
    }
}
