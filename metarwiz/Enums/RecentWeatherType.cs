using System.ComponentModel;

namespace ZippyNeuron.Metarwiz.Enums
{
    public enum RecentWeatherType
    {
        [Description("Unspecified")]
        Unspecified,
        [Description("Recent Freezing Drizzle")]
        REFZDZ,
        [Description("Recent Freezing Rain")]
        REFZRA,
        [Description("Recent Drizzle (Moderate or Heavy)")]
        REDZ,
        [Description("Recent Rain (Moderate or Heavy)")]
        RERA,
        [Description("Recent Snow (Moderate or Heavy)")]
        RESN,
        [Description("Recent Rain and Snow (Moderate or Heavy)")]
        RERASN,
        [Description("Recent Snow Grains (Moderate or Heavy)")]
        RESG,
        [Description("Recent Ice Pellets (Moderate or Heavy)")]
        REPL,
        [Description("Recent Rain Showers (Moderate or Heavy)")]
        RESHRA,
        [Description("Recent Snow Showers (Moderate or Heavy)")]
        RESHSN,
        [Description("Recent Showers of Hail (Moderate or Heavy)")]
        RESHGR,
        [Description("Recent Showers of Small hail and/or Snow Pellets (Moderate or Heavy)")]
        RESHGS,
        [Description("Recent Blowing Snow")]
        REBLSN,
        [Description("Recent Sandstorm")]
        RESS,
        [Description("Recent Duststorm")]
        REDS,
        [Description("Recent Thunderstorm with Rain")]
        RETSRA,
        [Description("Recent Thunderstorm with Snow")]
        RETSSN,
        [Description("Recent Thunderstorm with Hail")]
        RETSGR,
        [Description("Recent Thunderstorm with Small Hail")]
        RETSGS,
        [Description("Recent Thunderstorm Without Precipitation")]
        RETS,
        [Description("Recent Funnel Cloud (Tornado or Waterspout)")]
        REFC,
        [Description("Recent Volcanic Ash")]
        REVA,
        [Description("Recent Unidentified Precipitation")]
        REUP,
        [Description("Recent Freezing Rain with Unidentified Precipitation")]
        REFZUP,
        [Description("Recent Thunderstorm with Unidentified Precipitation")]
        RETSUP,
        [Description("Recent Showers of Unidentified Precipitation")]
        RESHUP
    }
}
