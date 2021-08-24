using System.ComponentModel;

namespace ZippyNeuron.Metarwiz.Enums
{
    public enum WeatherCharacteristicType
    {
        [Description("Unspecified")]
        Unspecified,
        [Description("Thunderstorm")]
        TS,
        [Description("Shower")]
        SH,
        [Description("Freezing")]
        FZ,
        [Description("Blowing")]
        BL,
        [Description("Low Drifting")]
        DR,
        [Description("Shallow")]
        MI,
        [Description("Patches")]
        BC,
        [Description("Partial")]
        PR,
    }
}
