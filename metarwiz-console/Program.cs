using System;
using System.Collections.Generic;
using ZippyNeuron.Metarwiz;
using ZippyNeuron.Metarwiz.Parser.Groups;
using ZippyNeuron.Metarwiz.Parser.Metars;
using ZippyNeuron.Metarwiz.Parser.Remarks;

namespace metarwiz_console
{
    class Program
    {
        static void Main(string[] args)
        {
            string metar = (args.Length > 0) ? String.Join(" ", args) : "METAR KATL 281152Z 11004KT 1SM R09R/P6000FT BR BKN004 24/23 A3020 RMK AO2 SFC VIS 4 SLP215 VIS N-E 1 1/2 CIG 003V005 TWRINC T02390228 10250 20239 53012";

            IMetarwiz mw = new Metarwiz(metar);

            MwLocation l = mw.Get<MwLocation>();
            if (l != null)
                Out("Station", l.ICAO);

            MwTimeOfObservation o = mw.Get<MwTimeOfObservation>();
            if (o != null)
                Out("Report", $"Issued at {new TimeSpan(o.Hour, o.Minute, 0)} on Day {o.Day}");

            MwSurfaceWind s = mw.Get<MwSurfaceWind>();
            if (s != null)
                Out("Wind", $"{s.Speed} in {s.UnitsDescription} from {s.Direction} degrees and is {((s.IsVariable) ? "" : "not ")}variable{((s.Gusting > 0) ? $", gusting at {s.Gusting}" : "")}");

            MwStatuteMiles m = mw.Get<MwStatuteMiles>();
            if (m != null)
                Out("Statute Miles (Visibility)", $"{m.Distance}");

            IEnumerable<MwCloud> c = mw.GetMany<MwCloud>();
            if (c != null)
                foreach (MwCloud cloud in c)
                    Out($"Cloud Layer", $"{cloud.CloudDescription} at {cloud.AboveGroundLevel.ToString("N0")}");

            MwTemperature t = mw.Get<MwTemperature>();
            if (t != null)
            {
                Out("Temperature (Celsius)", $"{t.Celsius} degrees");
                Out("Dew Point (Celsius)", $"{t.DewPoint} degrees");
            }

            MwPressure p = mw.Get<MwPressure>();
            if (p != null)
                Out("Pressure", $"hPa {p.HPa} inHg {p.InHg}");

            RwAutomatedStation a = mw.Get<RwAutomatedStation>();
            if (a != null)
                Out("Automation Station", $"{((a.HasRainSnowSensor) ? "Has a rain/snow sensor" : "Does not have a rain/snow sensor")}");

            RwSeaLevelPressure slp = mw.Get<RwSeaLevelPressure>();
            if (slp != null)
                Out("Sea Level Pressure", $"hPa {slp.HPa} inHg {slp.InHg}");

            RwHourlyTemperature ht = mw.Get<RwHourlyTemperature>();
            if (ht != null)
            {
                Out("Hourly Temperature (Celsius)", $"{ht.Celsius} degrees");
                Out("Hourly Dew Point (Celsius)", $"{ht.DewPoint} degrees");
            }

            RwSixHourMinTemperature shmin = mw.Get<RwSixHourMinTemperature>();
            if (shmin != null)
                Out("6 Hour Min Temperature (Celsius)", $"{shmin.Celsius} degrees");

            RwSixHourMaxTemperature shmax = mw.Get<RwSixHourMaxTemperature>();
            if (shmax != null)
                Out("6 Hour Max Temperature (Celsius)", $"{shmax.Celsius} degrees");

            RwPressureTendency pt = mw.Get<RwPressureTendency>();
            if (pt != null)
                Out("Pressure Tendency", $"{pt.TypeDescription} - hPa {pt.HPa} inHg {pt.InHg}");

            GwVariableCeiling v = mw.Get<GwVariableCeiling>();
            if (v != null)
                Out("Variable Ceiling", $"From {v.From} To {v.To}");

            GwSurfaceTowerVisibility sv = mw.Get<GwSurfaceTowerVisibility>();
            if (sv != null)
                Out("Surface Visibility", $"{sv.Distance}");
        }

        private static void Out(string label, string value)
        {
            Console.WriteLine($"{label.PadRight(36)}{value}");
        }
    }
}
