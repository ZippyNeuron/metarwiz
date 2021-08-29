using System;
using System.Collections.Generic;
using ZippyNeuron.Metarwiz;
using ZippyNeuron.Metarwiz.Parser.Metar;
using ZippyNeuron.Metarwiz.Parser.Remarks;

namespace metarwiz_console
{
    class Program
    {
        static void Main(string[] args)
        {
            IMetarwiz mw = new Metarwiz("METAR KORD 281151Z 20006G32KT 10SM FEW040 FEW070 SCT250 24/22 A3008 RMK AO2 SLP181 T02440217 10272 20244 53012");

            MwLocation l = mw.Get<MwLocation>();
            Out("Station", l.ICAO);

            MwTimeOfObservation o = mw.Get<MwTimeOfObservation>();
            Out("Observation Time", $"Day {o.Day} Time {new TimeSpan(o.Hour, o.Minute, 0)}");

            MwSurfaceWind s = mw.Get<MwSurfaceWind>();
            Out("Wind", $"{s.Speed} in {s.UnitsDescription} from {s.Direction} degrees and is {((s.IsVariable) ? "" : "not ")}variable{((s.Gusting > 0) ? $", gusting at {s.Gusting}" : "")}");

            MwStatuteMiles m = mw.Get<MwStatuteMiles>();
            Out("Statute Miles (Visibility)", $"{m.Distance}");

            IEnumerable<MwCloud> c = mw.GetMany<MwCloud>();
            foreach(MwCloud cloud in c)
                Out($"Cloud Layer", $"{cloud.CloudDescription} at {(cloud.AboveGroundLevel.ToString("N0"))}");

            MwTemperature t = mw.Get<MwTemperature>();
            Out("Temperature (Celsius)", $"{t.Celsius} degrees");
            Out("Dew Point (Celsius)", $"{t.DewPoint} degrees");

            MwPressure p = mw.Get<MwPressure>();
            Out("Pressure", $"hPa {p.HPa} inHg {p.InHg}");

            RwAutomatedStation a = mw.Get<RwAutomatedStation>();
            Out("Automation Station", $"{((a.HasRainSnowSensor) ? "Has a rain/snow sensor" : "Does not have a rain/snow sensor")}");

            RwSeaLevelPressure slp = mw.Get<RwSeaLevelPressure>();
            Out("Sea Level Pressure", $"hPa {slp.HPa} inHg {slp.InHg}");

            RwHourlyTemperature ht = mw.Get<RwHourlyTemperature>();
            Out("Hourly Temperature (Celsius)", $"{ht.Celsius} degrees");
            Out("Hourly Dew Point (Celsius)", $"{ht.DewPoint} degrees");

            RwSixHourMinTemperature shmin = mw.Get<RwSixHourMinTemperature>();
            Out("6 Hour Min Temperature (Celsius)", $"{shmin.Celsius} degrees");

            RwSixHourMaxTemperature shmax = mw.Get<RwSixHourMaxTemperature>();
            Out("6 Hour Max Temperature (Celsius)", $"{shmax.Celsius} degrees");

            RwPressureTendency pt = mw.Get<RwPressureTendency>();
            Out("Pressure Tendency", $"{pt.TypeDescription} - hPa {pt.HPa} inHg {pt.InHg}");
        }

        private static void Out(string label, string value)
        {
            Console.WriteLine($"{label.PadRight(36)}{value}");
        }
    }
}
