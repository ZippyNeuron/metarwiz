using System;
using System.Collections.Generic;
using ZippyNeuron.Metarwiz.Parser.Groups;
using ZippyNeuron.Metarwiz.Parser.Metars;
using ZippyNeuron.Metarwiz.Parser.Remarks;

namespace ZippyNeuron.Metarwiz.Parser
{
    public static class MetarParserItemTypes
    {
        public static IEnumerable<Type> MetarGroup = new Type[]
        {
            /* first */
            typeof(MwLocation),
            typeof(MwMetar),

            /* second */
            typeof(MwAutoOrNil),
            typeof(MwCavok),
            typeof(MwCloud),
            typeof(MwNoSig),
            typeof(MwPressure),
            typeof(MwRecentWeather),
            typeof(MwRunwayStateGroup),
            typeof(MwRunwayVisualRange),
            typeof(MwStatuteMiles),
            typeof(MwSurfaceWind),
            typeof(MwTemperature),
            typeof(MwTempo),
            typeof(MwTimeOfObservation),
            typeof(MwVisibility),
            typeof(MwWeather),
            typeof(MwWindVariation),
        };

        public static IEnumerable<Type> RemarksGroup = new Type[]
        {
            /* groups */
            typeof(GwSurfaceTowerVisibility),
            typeof(GwTornadic),
            typeof(GwVariableCeiling),
            typeof(GwPeakWind),
            typeof(GwWindShift),

            typeof(RwAutomatedStation),
            typeof(RwHourlyPrecipitation),
            typeof(RwHourlyTemperature),
            typeof(RwNeedsMaintenance),
            typeof(RwPressureTendency),
            typeof(RwRainBegan),
            typeof(RwRainEnded),
            typeof(RwSeaLevelPressure),
            typeof(RwSixHourMaxTemperature),
            typeof(RwSixHourMinTemperature),
            typeof(RwSixHourPrecipitation),
            typeof(RwTwentyFourHourPrecipitation),
            typeof(RwRemarks),
        };
    }
}
