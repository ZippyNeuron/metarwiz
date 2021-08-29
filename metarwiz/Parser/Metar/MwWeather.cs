using System;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Metar
{
    public class MwWeather : MwMetarItem
    {
        private readonly string _intensity;
        private readonly string _characteristic;
        private readonly string _vacinity;
        private readonly string _weather1;
        private readonly string _weather2;

        public MwWeather(int position, string value) : base(position, value, Pattern)
        {
            _intensity = Groups["INTENSITY"].Value;
            _characteristic = Groups["CHARACTERISTIC"].Value;
            _vacinity = Groups["VACINITY"].Value;
            _weather1 = Groups["WEATHER1"].Value;
            _weather2 = Groups["WEATHER2"].Value;
        }

        public bool IsInVacinity => _vacinity == "VC";

        public WeatherCharacteristicType Characteristic => (!String.IsNullOrEmpty(_characteristic)) ? Enum.Parse<WeatherCharacteristicType>(_characteristic) : WeatherCharacteristicType.Unspecified;

        public WeatherType WeatherPrimary => (!String.IsNullOrEmpty(_weather1)) ? Enum.Parse<WeatherType>(_weather1) : WeatherType.Unspecified;

        public WeatherType WeatherSecondary => (!String.IsNullOrEmpty(_weather2)) ? Enum.Parse<WeatherType>(_weather2) : WeatherType.Unspecified;

        public WeatherIntensityIndicator Intensity => _intensity switch
            {
                "-" => WeatherIntensityIndicator.Light,
                "+" => WeatherIntensityIndicator.Heavy,
                _ => WeatherIntensityIndicator.Moderate
            };

        private static string GetPattern()
        {
            string characteristics = String
                .Join("|", Enum.GetNames<WeatherCharacteristicType>());

            string weathers = String
                .Join("|", Enum.GetNames<WeatherType>());

            return $@"^(?<INTENSITY>\-|\+|)?(?<VACINITY>VC)?(?<CHARACTERISTIC>{characteristics})?(?<WEATHER1>{weathers})?(?<WEATHER2>{weathers})?$";
        }

        public static string Pattern => GetPattern();

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                Intensity.GetDescription(),
                _vacinity,
                (Characteristic != WeatherCharacteristicType.Unspecified) ? Enum.GetName<WeatherCharacteristicType>(Characteristic) : String.Empty,
                (WeatherPrimary != WeatherType.Unspecified) ? Enum.GetName<WeatherType>(WeatherPrimary) : String.Empty,
                (WeatherSecondary != WeatherType.Unspecified) ? Enum.GetName<WeatherType>(WeatherSecondary) : String.Empty
            );
        }
    }
}
