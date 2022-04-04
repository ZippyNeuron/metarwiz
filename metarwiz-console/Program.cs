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
            string metar = (args.Length > 0) ? String.Join(" ", args) : "METAR EGLC 221630Z AUTO 24008G36KT 350V070 1/2SM R32L/M0400V1200D +VCFG -RA BKN025 SCT030CB M01/M10 Q1022 REFZRA WS R18C W15/S2 R24L/123456";

            IMetarwiz mw = new Metarwiz(metar);

            MwLocation l = mw.Get<MwLocation>();
            if (l != null)
                Out("Station", l.ICAO);

            MwTimeOfObservation o = mw.Get<MwTimeOfObservation>();
            if (o != null)
                Out("Report", $"Issued at {new TimeSpan(o.Hour, o.Minute, 0)} on Day {o.Day}");

            MwSurfaceWindGroup s = mw.Get<MwSurfaceWindGroup>();
            if (s != null)
                Out("Wind", $"{s.Speed} in {s.UnitsDescription} from {s.Direction} degrees and is {((s.IsVariable) ? "" : "not ")}variable{((s.Gusting > 0) ? $", gusting at {s.Gusting}" : "")}");

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
                Out("Automation Station", $"{((a.HasPrecipitationDiscriminator) ? "Has a rain/snow sensor" : "Does not have a rain/snow sensor")}");

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

            RwVariableCeilingGroup v = mw.Get<RwVariableCeilingGroup>();
            if (v != null)
                Out("Variable Ceiling", $"From {v.From} To {v.To}");

            RwSurfaceTowerVisibilityGroup sv = mw.Get<RwSurfaceTowerVisibilityGroup>();
            if (sv != null)
                Out("Surface Visibility", $"{sv.Distance}");

            RwPeakWindGroup pw = mw.Get<RwPeakWindGroup>();
            if (pw != null)
                Out("Peak Wind", $"Direction {pw.Direction} degrees. Speed {pw.Speed} knots at {pw.Time}");

            RwTornadicGroup tor = mw.Get<RwTornadicGroup>();
            if (tor != null)
                Out("Tornadic Observation", $"{tor.ActivityDescription} at {tor.Distance} statute miles, heading {tor.Movement}");

            IEnumerable<MwRecentWeather> rw = mw.GetMany<MwRecentWeather>();
            if (c != null)
                foreach (MwRecentWeather w in rw)
                    Out($"Recent Weather", $"{w.Kind} at {w.KindDescription}");
        }

        private static void Out(string label, string value)
        {
            Console.WriteLine($"{label.PadRight(36)}{value}");
        }
    }
}
