using ZippyNeuron.Metarwiz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZippyNeuron.Metarwiz.Parser;

namespace metarwiz.tests
{
    [TestClass]
    public class TMetarInfo
    {
        public const string _metar = @"METAR EGLC 012050Z AUTO 06007KT 290V100 9999 OVC033 16/12 Q1030 RMK SLP021=";

        [TestMethod]
        public void Get_MetarInfo()
        {
            /* arrange */
            Metarwiz m = new(_metar, "Wibble");

            /* act */
            MetarInfo mi = m.Metar;

            /* assert */
            Assert.IsNotNull(mi);
            Assert.AreEqual("Wibble", mi.Tag);
            Assert.AreEqual(_metar, mi.Original);
            Assert.IsFalse(string.IsNullOrEmpty(mi.Metar));
            Assert.IsFalse(string.IsNullOrEmpty(mi.Remarks));
            Assert.IsTrue(mi.HasRemarks);
            Assert.IsTrue(mi.HasTerminator);
            Assert.AreEqual("=", mi.Terminator);
            Assert.AreEqual(string.Concat(mi.Metar, " ", mi.Remarks, mi.Terminator), _metar);
            Assert.AreEqual(mi.ToString(), _metar);
            Assert.AreEqual(_metar, mi.Original);
        }
    }
}
