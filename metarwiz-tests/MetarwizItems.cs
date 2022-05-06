using System;
using ZippyNeuron.Metarwiz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZippyNeuron.Metarwiz.Parser.Metars;
using ZippyNeuron.Metarwiz.Enums;
using System.Collections.Generic;
using System.Text.Json;
using ZippyNeuron.Metarwiz.Parser.Remarks;

namespace metarwiz.tests
{
    [TestClass]
    public class MetarwizItems
    {
        [TestMethod]
        [DataRow(@"METAR", "METAR")]
        public void MwMetar_Valid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwMetar o = m.Get<MwMetar>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.Value, value);
        }

        [TestMethod]
        [DataRow(@"")]
        [DataRow(@" ")]
        [DataRow(@" METAR")]
        [DataRow(@"METER")]
        [DataRow(@"META")]
        public void MwMetar_Invalid(string metar)
        {
            /* arrange */
            Metarwiz m;

            /* act */
            Action a = () => {
                m = new(metar);
            };

            /* assert */
            Assert.ThrowsException<MetarException>(a);
        }

        [TestMethod]
        [DataRow(@"METAR EGLC", "EGLC")]
        public void MwLocation_Valid(string metar, string location)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwLocation o = m.Get<MwLocation>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.ICAO, location);
        }

        [TestMethod]
        [DataRow(@"METAR EGLC 031822Z", 3, 18, 22)]
        [DataRow(@"METAR EGLC 312359Z", 31, 23, 59)]
        [DataRow(@"METAR EGLC 010000Z", 1, 0, 0)]
        public void MwTimeOfObservation_Valid(string metar, int day, int hour, int minute)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwTimeOfObservation o = m.Get<MwTimeOfObservation>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.Day, day);
            Assert.AreEqual(o.Hour, hour);
            Assert.AreEqual(o.Minute, minute);
        }

        [TestMethod]
        [DataRow(@"METAR EGLC AUTO", true, false)]
        [DataRow(@"METAR EGLC NIL", false, true)]
        public void MwAutoOrNil_Valid(string metar, bool isAuto, bool isNil)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwAutoOrNil o = m.Get<MwAutoOrNil>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.IsAuto, isAuto);
            Assert.AreEqual(o.IsNil, isNil);
        }

        [TestMethod]
        [DataRow(@"METAR EGLC 02005MPS 020V040", false, 20, 5, false, 0, false, SpeedUnit.MPS, 20, 40)]
        [DataRow(@"METAR EGLC 24004MPS", false, 240, 4, false, 0, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC VRB01MPS", true, 0, 1, false, 0, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 24008KT", false, 240, 8, false, 0, false, SpeedUnit.KT, 0, 0)]
        [DataRow(@"METAR EGLC VRB02KT", true, 0, 2, false, 0, false, SpeedUnit.KT, 0, 0)]
        [DataRow(@"METAR EGLC 19006MPS", false, 190, 6, false, 0, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 19012KT", false, 190, 12, false, 0, false, SpeedUnit.KT, 0, 0)]
        [DataRow(@"METAR EGLC 00000MPS", false, 0, 0, false, 0, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 00000KT", false, 0, 0, false, 0, false, SpeedUnit.KT, 0, 0)]
        [DataRow(@"METAR EGLC 140P149MPS", false, 140, 149, true, 0, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 140P99KT 040V060", false, 140, 99, true, 0, false, SpeedUnit.KT, 40, 60)]
        [DataRow(@"METAR EGLC 12003G09MPS", false, 120, 3, false, 9, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 12006G18KT", false, 120, 6, false, 18, false, SpeedUnit.KT, 0, 0)]
        [DataRow(@"METAR EGLC 24008G14MPS", false, 240, 8, false, 14, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 24016G28KT", false, 240, 16, false, 28, false, SpeedUnit.KT, 0, 0)]
        [DataRow(@"METAR EGLC 02005MPS", false, 20, 5, false, 0, false, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 02010KT", false, 20, 10, false, 0, false, SpeedUnit.KT, 0, 0)]
        [DataRow(@"METAR EGLC 24008GP99MPS", false, 240, 8, false, 99, true, SpeedUnit.MPS, 0, 0)]
        [DataRow(@"METAR EGLC 180P99GP99KT 000V100", false, 180, 99, true, 99, true, SpeedUnit.KT, 0, 100)]
        [DataRow(@"METAR EGLC 120P144G18KT", false, 120, 144, true, 18, false, SpeedUnit.KT, 0, 0)]
        public void MwSurfaceWindGroup_IsValid(string metar, bool variable, int direction, int speed, bool sexceeds, int gusting, bool gexceeds, SpeedUnit units, int from, int to)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwSurfaceWindGroup o = m.Get<MwSurfaceWindGroup>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.IsVariable, variable);
            Assert.AreEqual(o.Direction, direction);
            Assert.AreEqual(o.Speed, speed);
            Assert.AreEqual(o.SpeedExceeds100, sexceeds);
            Assert.AreEqual(o.Gusting, gusting);
            Assert.AreEqual(o.GustingExceeds100, gexceeds);
            Assert.AreEqual(o.Units, units);
            Assert.AreEqual(o.From, from);
            Assert.AreEqual(o.To, to);
        }

        [TestMethod]
        [DataRow(@"METAR EGLC 04005MPS 1234", false, 1234, 0, "", false, false)]
        [DataRow(@"METAR EGLC 02005MPS 1234 4321", false, 1234, 4321, "", false, false)]
        [DataRow(@"METAR EGLC 02005MPS 1234 2121NW", false, 1234, 2121, "NW", false, true)]
        [DataRow(@"METAR EGLC 02005MPS 9999 4321NE", false, 9999, 4321, "NE", true, true)]
        [DataRow(@"METAR EGLC 02005MPS CAVOK", true, 0, 0, "", false, false)]
        [DataRow(@"METAR EGLC 02005MPS 10SM", false, 16093, 0, "", true, false)]
        [DataRow(@"METAR EGLC 02005MPS 1/2SM", false, 805, 0, "", false, false)]
        public void MwVisibilityGroup_IsValid(string metar, bool isCAVOK, int minVisibility, int dirVisibility, string direction, bool isMinimumVisibilityMoreThan10K, bool hasDirectionVisibility)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwVisibilityGroup o = m.Get<MwVisibilityGroup>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.IsCAVOK, isCAVOK);
            Assert.AreEqual(o.MinimumVisibility, minVisibility);
            Assert.AreEqual(o.DirectionVisibility, dirVisibility);
            Assert.AreEqual(o.Direction, direction);
            Assert.AreEqual(o.IsMinimumVisibilityMoreThan10K, isMinimumVisibilityMoreThan10K);
            Assert.AreEqual(o.HasDirectionVisibility, hasDirectionVisibility);
        }

        [Serializable]
        public record MwRunwayVisualRangeData
        {
            public int Runway { get; set; }
            public string Designator { get; set; }
            public string Observation { get; set; }
            public int Range { get; set; }
            public string Tendency { get; set; }
            public int To { get; set; }
        }

        [TestMethod]
        [DataRow(@"METAR EGLC R12L/M0400 R30/0800U R28C/M0060V1200U", "[" +
            "{\"Runway\":12, \"Designator\":\"L\", \"Observation\":\"M\", \"Range\":400, \"Tendency\":\"Unspecified\", \"To\":0 }," +
            "{\"Runway\":30, \"Designator\":\"U\", \"Observation\":\"U\", \"Range\":800, \"Tendency\":\"U\", \"To\":0 }," +
            "{\"Runway\":28, \"Designator\":\"C\", \"Observation\":\"M\", \"Range\":60, \"Tendency\":\"U\", \"To\":1200 }" +
        "]")]
        [DataRow(@"METAR EGLC R10/9999", "[" +
            "{\"Runway\":10, \"Designator\":\"U\", \"Observation\":\"U\", \"Range\":9999, \"Tendency\":\"Unspecified\", \"To\":0 }" +
        "]")]
        public void MwRunwayVisualRange_IsValid(string metar, string jsonData)
        {
            /* arrange */
            MwRunwayVisualRangeData[] data = JsonSerializer.Deserialize<MwRunwayVisualRangeData[]>(jsonData);
            Metarwiz m = new(metar);
            int index = 0;

            /* act */
            IEnumerable<MwRunwayVisualRange> rvrs = m.GetMany<MwRunwayVisualRange>();

            /* assert */
            Assert.IsNotNull(rvrs);
            foreach(MwRunwayVisualRange rvr in rvrs) {
                Assert.AreEqual(rvr.Runway, data[index].Runway);
                Assert.AreEqual(rvr.Designator, Enum.Parse<RunwayType>(data[index].Designator));
                Assert.AreEqual(rvr.Observation, Enum.Parse<ObservationType>(data[index].Observation));
                Assert.AreEqual(rvr.Range, data[index].Range);
                Assert.AreEqual(rvr.Tendency, Enum.Parse<TendencyIndicator>(data[index].Tendency));
                Assert.AreEqual(rvr.To, data[index].To);
                index++;
            }
        }

        [Serializable]
        public record MwWeatherData
        {
            public bool IsInVacinity { get; set; }
            public string WeatherPrimary { get; set; }
            public string WeatherSecondary { get; set; }
            public string Intensity { get; set; }
        }

        [TestMethod]
        [DataRow(@"METAR EGLC 04005MPS +VCFG -RA", "[" +
            "{\"IsInVacinity\":true, \"WeatherPrimary\":\"FG\", \"WeatherSecondary\":\"Unspecified\", \"Intensity\":\"Heavy\" }," +
            "{\"IsInVacinity\":false, \"WeatherPrimary\":\"RA\", \"WeatherSecondary\":\"Unspecified\", \"Intensity\":\"Light\" }" +
        "]")]
        public void MwWeather_IsValid(string metar, string jsonData)
        {
            /* arrange */
            MwWeatherData[] data = JsonSerializer.Deserialize<MwWeatherData[]>(jsonData);
            Metarwiz m = new(metar);
            int index = 0;

            /* act */
            IEnumerable<MwWeather> weathers = m.GetMany<MwWeather>();

            /* assert */
            Assert.IsNotNull(weathers);
            foreach(MwWeather weather in weathers) {
                Assert.AreEqual(weather.IsInVacinity, data[index].IsInVacinity);
                Assert.AreEqual(weather.WeatherPrimary, Enum.Parse<WeatherType>(data[index].WeatherPrimary));
                Assert.AreEqual(weather.WeatherSecondary, Enum.Parse<WeatherType>(data[index].WeatherSecondary));
                Assert.AreEqual(weather.Intensity, Enum.Parse<WeatherIntensityIndicator>(data[index].Intensity));
                index++;
            }
        }
    
        [Serializable]
        public record MwCloudData
        {
            public int AboveGroundLevel { get; set; }
            public string Cloud { get; set; }
            public string CloudType { get; set; }
        }

        [TestMethod]
        [DataRow("METAR EGLC 04005MPS FEW300 BKN009TCU VV/// /////CB BKN025///", "[" +
            "{\"AboveGroundLevel\":30000, \"Cloud\":\"FEW\", \"CloudType\":\"Unspecified\" }," +
            "{\"AboveGroundLevel\":900, \"Cloud\":\"BKN\", \"CloudType\":\"TCU\" }," +
            "{\"AboveGroundLevel\":0, \"Cloud\":\"VV\", \"CloudType\":\"Unspecified\" }," +
            "{\"AboveGroundLevel\":0, \"Cloud\":\"Unspecified\", \"CloudType\":\"CB\" }," +
            "{\"AboveGroundLevel\":2500, \"Cloud\":\"BKN\", \"CloudType\":\"Unspecified\" }" +
        "]")]
        public void MwCloud_IsValid(string metar, string jsonData)
        {
            /* arrange */
            MwCloudData[] data = JsonSerializer.Deserialize<MwCloudData[]>(jsonData);
            Metarwiz m = new(metar);
            int index = 0;

            /* act */
            IEnumerable<MwCloud> clouds = m.GetMany<MwCloud>();

            /* assert */
            Assert.IsNotNull(clouds);
            foreach(MwCloud cloud in clouds) {
                Assert.AreEqual(cloud.AboveGroundLevel, data[index].AboveGroundLevel);
                Assert.AreEqual(cloud.Cloud, Enum.Parse<Cloud>(data[index].Cloud));
                Assert.AreEqual(cloud.CloudType, Enum.Parse<CloudType>(data[index].CloudType));
                index++;
            }
        }
    
        [TestMethod]
        [DataRow("METAR EGLC 10/12", 10, 12)]
        [DataRow("METAR EGLC M08/12", -8, 12)]
        [DataRow("METAR EGLC 07/M09", 7, -9)]
        [DataRow("METAR EGLC M10/M12", -10, -12)]
        public void MwTemperature_IsValid(string metar, int celsius, int dewpoint)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwTemperature o = m.Get<MwTemperature>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.Celsius, celsius);
            Assert.AreEqual(o.DewPoint, dewpoint);
        }

        [Serializable]
        public record MwPressureData
        {
            public decimal HPa { get; set; }
            public decimal InHg { get; set; }
        }

        [TestMethod]
        [DataRow("METAR EGLC Q1023", "{\"HPa\": 1023, \"InHg\": 30.21}")]
        [DataRow("METAR EGLC A2992", "{\"HPa\": 1013, \"InHg\": 29.92}")]
        [DataRow("METAR EGLC Q1010", "{\"HPa\": 1010, \"InHg\": 29.83}")]
        [DataRow("METAR EGLC Q1001", "{\"HPa\": 1001, \"InHg\": 29.56}")]
        public void MwPressure_IsValid(string metar, string jsonData)
        {
            /* arrange */
            MwPressureData data = JsonSerializer.Deserialize<MwPressureData>(jsonData);
            Metarwiz m = new(metar);

            /* act */
            MwPressure o = m.Get<MwPressure>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(data.HPa, o.HPa);
            Assert.AreEqual(data.InHg, o.InHg);
        }

        [Serializable]
        public record MwRecentWeatherData
        {
            public string Kind { get; set; }
        }

        [TestMethod]
        [DataRow("METAR EGLC 04005MPS REFZDZ RESN REPL", "[" +
            "{\"Kind\":\"REFZDZ\"}," +
            "{\"Kind\":\"RESN\"}," +
            "{\"Kind\":\"REPL\"}" +
        "]")]
        public void MwRecentWeather_IsValid(string metar, string jsonData)
        {
            /* arrange */
            MwRecentWeatherData[] data = JsonSerializer.Deserialize<MwRecentWeatherData[]>(jsonData);
            Metarwiz m = new(metar);
            int index = 0;

            /* act */
            IEnumerable<MwRecentWeather> weathers = m.GetMany<MwRecentWeather>();

            /* assert */
            Assert.IsNotNull(weathers);
            foreach(MwRecentWeather w in weathers) {
                Assert.AreEqual(w.Kind, Enum.Parse<RecentWeatherType>(data[index].Kind));
                index++;
            }
        }
    
        [TestMethod]
        [DataRow("METAR EGLC Q1023 WS R03", 3, "U")]
        [DataRow("METAR EGLC Q1023 WS R22L", 22, "L")]
        [DataRow("METAR EGLC Q1023 WS R10R", 10, "R")]
        [DataRow("METAR EGLC Q1023 WS R36C", 36, "C")]
        [DataRow("METAR EGLC Q1023 WS ALL RWY", 0, "U")]
        public void MwWindShearGroup_IsValid(string metar, int runway, string designator)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwWindShearGroup o = m.Get<MwWindShearGroup>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.Runway, runway);
            Assert.AreEqual(o.Designator, Enum.Parse<RunwayType>(designator));
        }
    
        [TestMethod]
        [DataRow("METAR EGLC W15/S2", 15, 2)]
        [DataRow("METAR EGLC WM12/S1", -12, 1)]
        [DataRow("METAR EGLC W1/S9", 1, 9)]
        [DataRow("METAR EGLC WM02/S3", -2, 3)]
        public void MwStateOfSeaSurface_IsValid(string metar, int temp, int state)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwStateOfSeaSurface o = m.Get<MwStateOfSeaSurface>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.Celsius, temp);
            Assert.AreEqual(o.SeaState, state);
        }
    
        [Serializable]
        public record MwStateOfRunwayData
        {
            public string Deposit { get; set; }
        }

        [TestMethod]
        [DataRow("METAR EGLC R24L/123456", "1", "2", "34", "56")]
        public void MwStateOfRunway_IsValid(string metar, string deposit, string extent, string depth, string friction)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwStateOfRunway o = m.Get<MwStateOfRunway>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(o.Deposit, deposit);
            Assert.AreEqual(o.Extent, extent);
            Assert.AreEqual(o.Depth, depth);
            Assert.AreEqual(o.Friction, friction);
            Assert.AreEqual("R24L/123456", o.Value);
            Assert.AreEqual("R24L/123456", o.ToString());
        }

        [TestMethod]
        [DataRow("METAR EGLC NOSIG")]
        public void MwNoSig_IsValid(string metar)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            MwNoSig o = m.Get<MwNoSig>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual("NOSIG", o.Value);
            Assert.AreEqual("NOSIG", o.ToString());
        }

        [TestMethod]
        [DataRow("METAR EGLC RMK")]
        public void RwRemarks_IsValid(string metar)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwRemarks o = m.Get<RwRemarks>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual("RMK", o.Value);
            Assert.AreEqual("RMK", o.ToString());
        }

        [TestMethod]
        [DataRow("METAR EGLC RMK AO2")]
        public void RwAutomatedStation_IsValid(string metar)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwAutomatedStation o = m.Get<RwAutomatedStation>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(true, o.HasPrecipitationDiscriminator);
            Assert.AreEqual("AO2", o.Value);
            Assert.AreEqual("AO2", o.ToString());
        }

        [TestMethod]
        [DataRow("METAR EGLC RMK P1234", "P1234")]
        public void RwHourlyPrecipitation_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwHourlyPrecipitation o = m.Get<RwHourlyPrecipitation>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(false, o.IsTrace);
            Assert.AreEqual(12.34m, o.Inches);
            Assert.AreEqual(value, o.Value);
            Assert.AreEqual(value, o.ToString());
        }

        [TestMethod]
        [DataRow("METAR EGLC RMK T00640036", "T00640036")]
        public void RwHourlyTemperature_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwHourlyTemperature o = m.Get<RwHourlyTemperature>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(6.40m, o.Celsius);
            Assert.AreEqual(3.60m, o.DewPoint);
            Assert.AreEqual(value, o.Value);
            Assert.AreEqual(value, o.ToString());
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwNeedsMaintenance_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwNeedsMaintenance o = m.Get<RwNeedsMaintenance>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwPeakWindGroup_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwPeakWindGroup o = m.Get<RwPeakWindGroup>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwPressureTendency_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwPressureTendency o = m.Get<RwPressureTendency>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwSeaLevelPressure_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwSeaLevelPressure o = m.Get<RwSeaLevelPressure>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwSixHourMaxTemperature_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwSixHourMaxTemperature o = m.Get<RwSixHourMaxTemperature>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwSixHourMinTemperature_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwSixHourMinTemperature o = m.Get<RwSixHourMinTemperature>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwSixHourPrecipitation_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwSixHourPrecipitation o = m.Get<RwSixHourPrecipitation>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwSurfaceTowerVisibilityGroup_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwSurfaceTowerVisibilityGroup o = m.Get<RwSurfaceTowerVisibilityGroup>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwTornadicGroup_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwTornadicGroup o = m.Get<RwTornadicGroup>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwTwentyFourHourPrecipitation_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwTwentyFourHourPrecipitation o = m.Get<RwTwentyFourHourPrecipitation>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwVariableCeilingGroup_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwVariableCeilingGroup o = m.Get<RwVariableCeilingGroup>();

            /* assert */
            Assert.IsNotNull(o);
        }

        [TestMethod]
        [DataRow("METAR EGLC", "")]
        public void RwWindShiftGroup_IsValid(string metar, string value)
        {
            /* arrange */
            Metarwiz m = new(metar);

            /* act */
            RwWindShiftGroup o = m.Get<RwWindShiftGroup>();

            /* assert */
            Assert.IsNotNull(o);
        }
    }
}
