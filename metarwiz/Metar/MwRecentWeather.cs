using System;
using ZippyNeuron.Metarwiz.Abstractions;
using ZippyNeuron.Metarwiz.Enums;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwRecentWeather : MvMetarItem
    {
        private readonly string _supplementary;

        public MwRecentWeather(int position, string value) : base(position, value, Pattern)
        {
            _supplementary = Groups["SUPPLEMENTARY"].Value;
        }

        public RecentWeatherType Kind => (!String.IsNullOrEmpty(_supplementary)) ? Enum.Parse<RecentWeatherType>(_supplementary) : RecentWeatherType.Unspecified;

        private static string GetPattern()
        {
            string supplementaries = String
                .Join("|", Enum.GetNames<RecentWeatherType>());

            return $@"^(?<SUPPLEMENTARY>{supplementaries})$";
        }

        public static string Pattern => GetPattern();

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return Enum.GetName<RecentWeatherType>(Kind);
        }
    }
}
