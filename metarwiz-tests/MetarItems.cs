using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZippyNeuron.Metarwiz;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Metar;
using ZippyNeuron.Metarwiz.Remarks;

namespace metarwiz.tests
{
    [TestClass]
    public class MetarItems
    {
        public const string _metar = @"METAR EGLC 221850Z R24R/M1200D 10SM TEMPO AUTO VCTS 29005G26KT NOSIG 250V320 CAVOK BKN012 FEW036 -VCSN +RA -TSRA VCFG 9999 NCD M19/16 SM01 Q1022 RESHSN 7649//93 RMK AO2 SLP093 P0003 60009 70009 T10640036 11066 21012 58033 $";

        [TestMethod]
        [DataRow(_metar)]
        public void Create_Metarwiz_Instance(string metar)
        {
            /* arrange */
            Metarwiz x = new();

            /* act */
            x.Parse(metar, "42");

            /* assert */
            Assert.IsNotNull(x);
            Assert.IsNotNull(x.Metar);
            Assert.IsTrue(!String.IsNullOrEmpty(x.Metar.Metar));
            Assert.IsTrue(x.Metar.HasRemarks);
            Assert.IsFalse(x.Metar.HasTerminator);
            Assert.AreEqual(x.Metar.Terminator, String.Empty);
            Assert.AreEqual(x.Metar.Tag, "42");
            Assert.IsTrue(!String.IsNullOrEmpty(x.Metar.Remarks));
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Create_Metarwiz_Instance_Constructor(string metar)
        {
            /* arrange */
            Metarwiz x;

            /* act */
            x = new(metar);

            /* assert */
            Assert.IsNotNull(x);
            Assert.IsNotNull(x.Metar);
            Assert.IsTrue(!String.IsNullOrEmpty(x.Metar.Metar));
            Assert.IsTrue(x.Metar.HasRemarks);
            Assert.IsFalse(x.Metar.HasTerminator);
            Assert.AreEqual(x.Metar.Terminator, String.Empty);
            Assert.AreEqual(x.Metar.Tag, null);
            Assert.IsTrue(!String.IsNullOrEmpty(x.Metar.Remarks));
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Create_Metarwiz_Static(string metar)
        {
            /* arrange */
            Metarwiz x;

            /* act */
            x = Metarwiz.Parse(metar);

            /* assert */
            Assert.IsNotNull(x);
            Assert.IsNotNull(x.Metar);
            Assert.IsTrue(!String.IsNullOrEmpty(x.Metar.Metar));
            Assert.IsTrue(x.Metar.HasRemarks);
            Assert.IsFalse(x.Metar.HasTerminator);
            Assert.AreEqual(x.Metar.Terminator, String.Empty);
            Assert.AreEqual(x.Metar.Tag, null);
            Assert.IsTrue(!String.IsNullOrEmpty(x.Metar.Remarks));
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwAuroOrNill(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwAutoOrNil x = m.Get<MwAutoOrNil>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.IsFalse(x.IsNil);
            Assert.IsTrue(x.IsAutomated);
            Assert.AreEqual(x.Value, "AUTO");
            Assert.AreEqual(x.ToString(), "AUTO");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwCavok(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwCavok x = m.Get<MwCavok>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Value, "CAVOK");
            Assert.AreEqual(x.ToString(), "CAVOK");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwNoSig(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwNoSig x = m.Get<MwNoSig>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Value, "NOSIG");
            Assert.AreEqual(x.ToString(), "NOSIG");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwTemporaryFluctuation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwTempo x = m.Get<MwTempo>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Value, "TEMPO");
            Assert.AreEqual(x.ToString(), "TEMPO");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwCloud(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            List<MwCloud> x = (List<MwCloud>)m.GetMany<MwCloud>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Count, 3);
            Assert.AreEqual(x[0].Cloud, CloudType.BKN);
            Assert.AreEqual(x[0].AboveGroundLevel, 1200);
            Assert.AreEqual(x[0].Value, "BKN012");
            Assert.AreEqual(x[0].ToString(), "BKN012");
            Assert.AreEqual(x[1].Cloud, CloudType.FEW);
            Assert.AreEqual(x[1].AboveGroundLevel, 3600);
            Assert.AreEqual(x[1].Value, "FEW036");
            Assert.AreEqual(x[1].ToString(), "FEW036");
            Assert.AreEqual(x[2].Cloud, CloudType.NCD);
            Assert.AreEqual(x[2].AboveGroundLevel, 0);
            Assert.AreEqual(x[2].Value, "NCD");
            Assert.AreEqual(x[2].ToString(), "NCD");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwLocation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwLocation x = m.Get<MwLocation>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.ICAO, "EGLC");
            Assert.AreEqual(x.Value, "EGLC");
            Assert.AreEqual(x.ToString(), "EGLC");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwPressure(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwPressure x = m.Get<MwPressure>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.HPa, 1022m);
            Assert.AreEqual(x.InHg, 30.18m);
            Assert.AreEqual(x.Value, "Q1022");
            Assert.AreEqual(x.ToString(), "Q1022");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwRecentWeather(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwRecentWeather x = m.Get<MwRecentWeather>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Kind, RecentWeatherType.RESHSN);
            Assert.AreEqual(x.Value, "RESHSN");
            Assert.AreEqual(x.ToString(), "RESHSN");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwReport(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwMetar x = m.Get<MwMetar>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Value, "METAR");
            Assert.AreEqual(x.ToString(), "METAR");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwRunwayStateGroup(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwRunwayStateGroup x = m.Get<MwRunwayStateGroup>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Bearing, 26);
            Assert.AreEqual(x.Deposit, "Dry Snow");
            Assert.AreEqual(x.Depth, 150);
            Assert.AreEqual(x.Extent, "51% to 100%");
            Assert.AreEqual(x.IsAllRunways, false);
            Assert.AreEqual(x.IsLeft, false);
            Assert.AreEqual(x.IsNoNewInformation, false);
            Assert.AreEqual(x.IsNoSpecificRunway, false);
            Assert.AreEqual(x.IsRight, true);
            Assert.AreEqual(x.Operational, true);
            Assert.AreEqual(x.Orientation, "R");
            Assert.AreEqual(x.Runway, "26R");
            Assert.AreEqual(x.Value, "7649//93");
            Assert.AreEqual(x.ToString(), "7649//93");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwRunwayVisualRange(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwRunwayVisualRange x = m.Get<MwRunwayVisualRange>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Designator, RunwayType.R);
            Assert.AreEqual(x.Observation, ObservationType.M);
            Assert.AreEqual(x.Range, 1200);
            Assert.AreEqual(x.Runway, 24);
            Assert.AreEqual(x.Tendency, TendencyIndicator.D);
            Assert.AreEqual(x.Value, "R24R/M1200D");
            Assert.AreEqual(x.ToString(), "R24R/M1200D");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwStatuteMiles(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwStatuteMiles x = m.Get<MwStatuteMiles>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Distance, 10);
            Assert.AreEqual(x.ToString(), "10SM");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwSurfaceWind(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwSurfaceWind x = m.Get<MwSurfaceWind>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Direction, 290);
            Assert.AreEqual(x.Gusting, 26);
            Assert.AreEqual(x.IsVariable, false);
            Assert.AreEqual(x.Units, SpeedUnit.KT);
            Assert.AreEqual(x.Speed, 5);
            Assert.AreEqual(x.Value, "29005G26KT");
            Assert.AreEqual(x.ToString(), "29005G26KT");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwTemperature(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwTemperature x = m.Get<MwTemperature>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Celsius, -19);
            Assert.AreEqual(x.DewPoint, 16);
            Assert.AreEqual(x.Value, "M19/16");
            Assert.AreEqual(x.ToString(), "M19/16");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwTimeOfObservation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwTimeOfObservation x = m.Get<MwTimeOfObservation>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Day, 22);
            Assert.AreEqual(x.Hour, 18);
            Assert.AreEqual(x.Minute, 50);
            Assert.AreEqual(x.Value, "221850Z");
            Assert.AreEqual(x.ToString(), "221850Z");
        }

        //[TestMethod]
        //[DataRow(_metar)]
        //public void Get_MwVacinityThunderstorm(string metar)
        //{
        //    /* arrange */
        //    Metarwiz m = Metarwiz.Parse(metar);

        //    /* act */
        //    MwVacinityThunderstorm x = m.Get<MwVacinityThunderstorm>();

        //    /* assert */
        //    Assert.IsNotNull(x);
        //    Assert.IsTrue(x.IsVCTS);
        //    Assert.AreEqual(x.Value, "VCTS");
        //    Assert.AreEqual(x.ToString(), "VCTS");
        //}

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwVisibility(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwVisibility x = m.Get<MwVisibility>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.IsMoreThanTenKilometres, true);
            Assert.AreEqual(x.Distance, 9999);
            Assert.AreEqual(x.Value, "9999");
            Assert.AreEqual(x.ToString(), "9999");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwWeather(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            List<MwWeather> x = (List<MwWeather>)m.GetMany<MwWeather>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Count, 5);
            Assert.AreEqual(x[0].Characteristic, WeatherCharacteristicType.TS);
            Assert.AreEqual(x[0].Intensity, WeatherIntensityIndicator.Moderate);
            Assert.AreEqual(x[0].IsInVacinity, true);
            Assert.AreEqual(x[0].WeatherPrimary, WeatherType.Unspecified);
            Assert.AreEqual(x[0].WeatherSecondary, WeatherType.Unspecified);
            Assert.AreEqual(x[0].Value, "VCTS");
            Assert.AreEqual(x[0].ToString(), "VCTS");
            Assert.AreEqual(x[1].Characteristic, WeatherCharacteristicType.Unspecified);
            Assert.AreEqual(x[1].Intensity, WeatherIntensityIndicator.Light);
            Assert.AreEqual(x[1].IsInVacinity, true);
            Assert.AreEqual(x[1].WeatherPrimary, WeatherType.SN);
            Assert.AreEqual(x[1].WeatherSecondary, WeatherType.Unspecified);
            Assert.AreEqual(x[1].Value, "-VCSN");
            Assert.AreEqual(x[1].ToString(), "-VCSN");
            Assert.AreEqual(x[2].Characteristic, WeatherCharacteristicType.Unspecified);
            Assert.AreEqual(x[2].Intensity, WeatherIntensityIndicator.Heavy);
            Assert.AreEqual(x[2].IsInVacinity, false);
            Assert.AreEqual(x[2].WeatherPrimary, WeatherType.RA);
            Assert.AreEqual(x[2].WeatherSecondary, WeatherType.Unspecified);
            Assert.AreEqual(x[2].Value, "+RA");
            Assert.AreEqual(x[2].ToString(), "+RA");
            Assert.AreEqual(x[3].Characteristic, WeatherCharacteristicType.TS);
            Assert.AreEqual(x[3].Intensity, WeatherIntensityIndicator.Light);
            Assert.AreEqual(x[3].IsInVacinity, false);
            Assert.AreEqual(x[3].WeatherPrimary, WeatherType.RA);
            Assert.AreEqual(x[3].WeatherSecondary, WeatherType.Unspecified);
            Assert.AreEqual(x[3].Value, "-TSRA");
            Assert.AreEqual(x[3].ToString(), "-TSRA");
            Assert.AreEqual(x[4].Characteristic, WeatherCharacteristicType.Unspecified);
            Assert.AreEqual(x[4].Intensity, WeatherIntensityIndicator.Moderate);
            Assert.AreEqual(x[4].IsInVacinity, true);
            Assert.AreEqual(x[4].WeatherPrimary, WeatherType.FG);
            Assert.AreEqual(x[4].WeatherSecondary, WeatherType.Unspecified);
            Assert.AreEqual(x[4].Value, "VCFG");
            Assert.AreEqual(x[4].ToString(), "VCFG");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_MwWindVariation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            MwWindVariation x = m.Get<MwWindVariation>();
            
            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.From, 250);
            Assert.AreEqual(x.To, 320);
            Assert.AreEqual(x.Value, "250V320");
            Assert.AreEqual(x.ToString(), "250V320");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwAutomatedStation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwAutomatedStation x = m.Get<RwAutomatedStation>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.IsTrue(x.HasRainSnowSensor);
            Assert.AreEqual(x.Value, "AO2");
            Assert.AreEqual(x.ToString(), "AO2");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwRemarks(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwRemarks x = m.Get<RwRemarks>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Value, "RMK");
            Assert.AreEqual(x.ToString(), "RMK");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwSeaLevelPressure(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwSeaLevelPressure x = m.Get<RwSeaLevelPressure>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.HPa, 1009m);
            Assert.AreEqual(x.InHg, 29.80m);
            Assert.AreEqual(x.Value, "SLP093");
            Assert.AreEqual(x.ToString(), "SLP093");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwHourlyPrecipitation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwHourlyPrecipitation x = m.Get<RwHourlyPrecipitation>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Inches, 0.03m);
            Assert.AreEqual(x.IsTrace, false);
            Assert.AreEqual(x.Value, "P0003");
            Assert.AreEqual(x.ToString(), "P0003");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwSixHourPrecipitation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwSixHourPrecipitation x = m.Get<RwSixHourPrecipitation>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Inches, 0.09m);
            Assert.AreEqual(x.IsTrace, false);
            Assert.AreEqual(x.Value, "60009");
            Assert.AreEqual(x.ToString(), "60009");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwTwentyFourHourPrecipitation(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwTwentyFourHourPrecipitation x = m.Get<RwTwentyFourHourPrecipitation>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Inches, 0.09m);
            Assert.AreEqual(x.IsTrace, false);
            Assert.AreEqual(x.Value, "70009");
            Assert.AreEqual(x.ToString(), "70009");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwNeedsMaintenance(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwNeedsMaintenance x = m.Get<RwNeedsMaintenance>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Value, "$");
            Assert.AreEqual(x.ToString(), "$");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwHourlyTemperature(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwHourlyTemperature x = m.Get<RwHourlyTemperature>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Celsius, -6.4m);
            Assert.AreEqual(x.DewPoint, 3.6m);
            Assert.AreEqual(x.Value, "T10640036");
            Assert.AreEqual(x.ToString(), "T10640036");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwSixHourMaxTemperature(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwSixHourMaxTemperature x = m.Get<RwSixHourMaxTemperature>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Celsius, -6.6m);
            Assert.AreEqual(x.Value, "11066");
            Assert.AreEqual(x.ToString(), "11066");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwSixHourMinTemperature(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwSixHourMinTemperature x = m.Get<RwSixHourMinTemperature>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.Celsius, -1.2m);
            Assert.AreEqual(x.Value, "21012");
            Assert.AreEqual(x.ToString(), "21012");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_RwPressureTendency(string metar)
        {
            /* arrange */
            Metarwiz m = Metarwiz.Parse(metar);

            /* act */
            RwPressureTendency x = m.Get<RwPressureTendency>();

            /* assert */
            Assert.IsNotNull(x);
            Assert.AreEqual(x.HPa, 1003m);
            Assert.AreEqual(x.InHg, 29.62m);
            Assert.AreEqual(x.Value, "58033");
            Assert.AreEqual(x.ToString(), "58033");
        }

        [TestMethod]
        [DataRow(_metar)]
        public void Get_Metar_Reconstructed(string metar)
        {
            /* arrange */
            Metarwiz x = new();

            /* act */
            x.Parse(metar, "42");

            /* assert */
            Assert.IsNotNull(x);
            Assert.IsNotNull(x.ToString(), _metar);
        }
    }
}
