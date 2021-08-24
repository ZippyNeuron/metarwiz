using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZippyNeuron.Metarwiz;

namespace metarwiz.tests
{
    [TestClass]
    public class Validator
    {
        /// <summary>
        /// Metar Validation - Standard (RMKs are not currently handled)
        /// </summary>
        /// <param name="metar">The full METAR report</param>
        [TestMethod]
        [DataRow(@"METAR EGLC 212035Z AUTO VRB05KT 250V320 R24R/M1200D BKN012 -VCSN +RA -TSRA VCFG M10/02 Q1012 RESHSN 7649//93=")]
        [DataRow(@"METAR EGLC 221850Z AUTO 29005KT 9999 NCD 19/16 Q1022")]
        [DataRow(@"METAR EGLC 212320Z AUTO 24007KT 9999 SCT034 BKN046 17/15 Q1015")]
        [DataRow(@"METAR EGLC 212350Z AUTO 23005KT 9999 FEW037 SCT046 17/15 Q1015")]
        [DataRow(@"METAR EGLC 220150Z AUTO 24007KT 9999 SCT012 BKN035 17/15 Q1015")]
        [DataRow(@"METAR EGLC 220350Z AUTO 30007KT 4100 RA BKN009/// OVC042/// //////CB 17/16 Q1015 RERA")]
        [DataRow(@"METAR EGLC 220420Z AUTO 30009KT 280V340 9999 -RA FEW010/// SCT033/// BKN043/// //////CB 16/14 Q1015 RERA")]
        [DataRow(@"METAR EGLC 220520Z AUTO 28007KT 9999 SCT014/// //////TCU 16/14 Q1015")]
        [DataRow(@"METAR EGLC 220620Z AUTO 28008KT 9999 FEW012 SCT020 17/15 Q1016")]
        [DataRow(@"METAR EGLC 220750Z AUTO 29010KT 9999 -RA OVC012 17/14 Q1017")]
        [DataRow(@"METAR EGLC 220920Z AUTO 29009KT 250V310 9999 OVC011 17/15 Q1018")]
        [DataRow(@"METAR EGLC 221220Z AUTO 31009KT 280V340 9999 -RA BKN033 OVC045 19/14 Q1019")]
        [DataRow(@"METAR EGLC 221450Z AUTO 30008KT 9999 //////CB 21/15 Q1020")]
        [DataRow(@"METAR EGLC 221520Z AUTO 30008KT 280V340 9999 FEW045/// //////TCU 21/15 Q1020")]
        [DataRow(@"METAR EGLC 221650Z AUTO 28005KT 250V010 9999 -RA FEW033/// SCT043/// //////CB 18/16 Q1021 RERA")]
        [DataRow(@"METAR KOAK 221153Z 24005KT 10SM OVC013 16/12 A2983")]

        public void MetarValidationICAO(string metar)
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = Metarwiz.Parse(metar);
            
            /* assert */
            Console.WriteLine(m.Metar.ToString(false));
            Console.WriteLine(m.ToString());

            Assert.AreEqual(m.Metar.ToString(), m.ToString());
        }

        /// <summary>
        /// Metar Validation - North American Extensions (RMKs are not currently handled)
        /// </summary>
        /// <param name="metar">The full METAR report</param>
        [TestMethod]
        [DataRow(@"METAR KOAK 222353Z 31013KT 10SM FEW013 SCT040 21/13 A2980 RMK AO2 SLP091 FU SCT040 T02110128 10222 20172 58013=")]
        [DataRow(@"METAR KOAK 230053Z 31013KT 10SM FEW013 FEW040 19/13 A2980 RMK AO2 SLP089 FU FEW040 T01940128")]
        [DataRow(@"METAR KOAK 230153Z 31013KT 10SM FEW004 FEW009 FEW040 18/13 A2980 RMK AO2 SLP090 FU FEW004 FU FEW040 T01780128")]
        [DataRow(@"METAR KOAK 230253Z 32011KT 10SM FEW006 FEW009 FEW050 16/12 A2981 RMK AO2 SLP093 FU FEW050 T01610122 53001")]
        [DataRow(@"METAR KOAK 230353Z 31013KT 10SM FEW009 FEW050 16/12 A2982 RMK AO2 SLP098 T01560122")]
        [DataRow(@"METAR KOAK 230453Z 32007KT 10SM FEW010 16/12 A2984 RMK AO2 SLP105 T01560122")]
        [DataRow(@"METAR KOAK 230553Z 35006KT 10SM FEW010 15/12 A2984 RMK AO2 SLP106 T01500122 10206 20150 51013")]
        [DataRow(@"METAR KLAX 232153Z 25012KT 10SM FEW012 FEW030 23/16 A2987 RMK AO2 SLP114 T02280156=")]
        public void MetarValidationNA(string metar)
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = Metarwiz.Parse(metar);


            /* assert */
            Console.WriteLine(m.Metar.ToString(false));
            Console.WriteLine(m.ToString());

            Assert.AreEqual(m.Metar.ToString(), m.ToString());
        }
    }
}
