using System.ComponentModel;

namespace ZippyNeuron.Metarwiz.Enums
{
    public enum WeatherType
    {
        [Description("Unspecified")]
        Unspecified,
        [Description("Drizzle")]
        DZ,
        [Description("Rain")]
        RA,
        [Description("Snow")]
        SN,
        [Description("Snow Grains")]
        SG,
        [Description("Ice Pellets")]
        PL,
        [Description("Hail")]
        GR,
        [Description("Small Hail and/or Snow Pellets")]
        GS,
        [Description("Unknown Precipitation")]
        UP,
        [Description("Fog")]
        FG,
        [Description("Mist")]
        BR,
        [Description("Sand")]
        SA,
        [Description("Dust (Widespread)")]
        DU,
        [Description("Haze")]
        HZ,
        [Description("Smoke")]
        FU,
        [Description("Volcanic Ash")]
        VA,
        [Description("Dust/Sand Whirls (Dust Devils)")]
        PO,
        [Description("Squall")]
        SQ,
        [Description("Funnel Cloud (Tornado or Waterspout)")]
        FC,
        [Description("Duststorm")]
        DS,
        [Description("Sandstorm")]
        SS,
    }
}
