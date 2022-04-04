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
            /* first - fixed items */
            typeof(MwLocation),
            typeof(MwMetar),

            /* groups - multiword items */
            typeof(MwSurfaceWindGroup),
            typeof(MwVisibilityGroup),
            typeof(MwWindShearGroup),

            /* standard - single word items */
            typeof(MwAutoOrNil),
            typeof(MwCloud),
            typeof(MwNoSig),
            typeof(MwPressure),
            typeof(MwRecentWeather),
            typeof(MwRunwayVisualRange),
            typeof(MwTemperature),
            typeof(MwTimeOfObservation),
            typeof(MwWeather),
            typeof(MwStateOfSeaSurface),
            typeof(MwStateOfRunway)
        };

        public static IEnumerable<Type> RemarksGroup = new Type[]
        {
            /* first - fixed items */
            typeof(RwRemarks),

            /* groups - multiword items */
            typeof(RwSurfaceTowerVisibilityGroup),
            typeof(RwTornadicGroup),
            typeof(RwVariableCeilingGroup),
            typeof(RwPeakWindGroup),
            typeof(RwWindShiftGroup),

            /* standard - single word items */
            typeof(RwAutomatedStation),
            typeof(RwHourlyPrecipitation),
            typeof(RwHourlyTemperature),
            typeof(RwNeedsMaintenance),
            typeof(RwPressureTendency),
            typeof(RwSeaLevelPressure),
            typeof(RwSixHourMaxTemperature),
            typeof(RwSixHourMinTemperature),
            typeof(RwSixHourPrecipitation),
            typeof(RwTwentyFourHourPrecipitation),
        };
    }
}
