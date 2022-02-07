using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZippyNeuron.Metarwiz;
using ZippyNeuron.Metarwiz.Parser;
using ZippyNeuron.Metarwiz.Parser.Metars;
using ZippyNeuron.Metarwiz.Enums;

namespace metarwiz.tests
{
    [TestClass]
    public class TMetarwiz
    {
        public const string _metar = @"METAR EGLC 012050Z AUTO 06007G22KT 290V100 9999 BKN018 OVC033 16/12 Q1030 RMK SLP021=";

        [TestMethod]
        public void Create_Default()
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = new Metarwiz();

            /* assert */
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void Create_WithMetar()
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = new(_metar);

            /* assert */
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void Create_WithMetarAndTag()
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = new(_metar, "Wibble");

            /* assert */
            Assert.IsNotNull(m);
            Assert.AreEqual("Wibble", m.Metar.Tag);
        }

        [TestMethod]
        public void Get_MetarInfo()
        {
            /* arrange */
            Metarwiz m = new(_metar);

            /* act */
            MetarInfo mi = m.Metar;

            /* assert */
            Assert.IsNotNull(mi);
        }

        [TestMethod]
        public void Get_Item()
        {
            /* arrange */
            Metarwiz m = new(_metar);

            /* act */
            MwSurfaceWind o = m.Get<MwSurfaceWind>();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(60, o.Direction);
            Assert.AreEqual(7, o.Speed);
            Assert.AreEqual(22, o.Gusting);
            Assert.IsFalse(o.IsVariable);
            Assert.AreEqual(SpeedUnit.KT, o.Units);
            Assert.AreEqual("Knots", o.UnitsDescription);
            Assert.AreEqual("06007G22KT", o.Value);
        }

        [TestMethod]
        public void Get_Items()
        {
            /* arrange */
            Metarwiz m = new(_metar);

            /* act */
            List<MwCloud> o = m.GetMany<MwCloud>().ToList();

            /* assert */
            Assert.IsNotNull(o);
            Assert.AreEqual(2, o.Count);

            Assert.AreEqual(1800, o[0].AboveGroundLevel);
            Assert.AreEqual(CloudType.BKN, o[0].Cloud);
            Assert.AreEqual("Broken", o[0].CloudDescription);
            Assert.AreEqual("BKN018", o[0].Value);

            Assert.AreEqual(3300, o[1].AboveGroundLevel);
            Assert.AreEqual(CloudType.OVC, o[1].Cloud);
            Assert.AreEqual("Overcast", o[1].CloudDescription);
            Assert.AreEqual("OVC033", o[1].Value);
        }

        [TestMethod]
        public void Instance_Parse()
        {
            /* arrange */
            Metarwiz m = new();

            /* act */
            m.Parse(_metar, "Wibble");

            /* assert */
            Assert.IsNotNull(m);
            Assert.AreEqual("Wibble", m.Metar.Tag);
        }

        [TestMethod]
        public void Static_Parse()
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = Metarwiz.Parse(_metar);

            /* assert */
            Assert.IsNotNull(m);
            Assert.IsNull(m.Metar.Tag);
        }

        [TestMethod]
        public void ToString_Parse()
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = Metarwiz.Parse(_metar);

            /* assert */
            Assert.IsNotNull(m);
            Assert.AreEqual(m.ToString(), _metar);
        }
    }
}
