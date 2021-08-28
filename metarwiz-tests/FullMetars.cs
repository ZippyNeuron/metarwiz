using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZippyNeuron.Metarwiz;

namespace metarwiz.tests
{
    [TestClass]
    public class FullMetars
    {
        [TestMethod]
        [DataRow(@"METAR EGPD 281050Z AUTO VRB03KT 9999 FEW009 SCT012 15/12 Q1028 NOSIG")]
        [DataRow(@"METAR EGAA 281050Z AUTO 26005KT 230V300 9999 NCD 18/13 Q1028")]
        [DataRow(@"METAR EGBB 281050Z 06007KT 010V090 9999 BKN020 16/12 Q1028")]
        [DataRow(@"METAR EGHH 281050Z 03010KT 9999 BKN020 17/11 Q1026")]
        [DataRow(@"METAR EGGD 281050Z AUTO 01007KT 340V050 9999 BKN009 16/12 Q1028")]
        [DataRow(@"METAR EGFF 281050Z AUTO 08009KT 040V110 9999 SCT015 16/12 Q1028")]
        [DataRow(@"METAR EGNX 281050Z 03006KT 340V100 9999 SCT022 SCT026 18/12 Q1028")]
        [DataRow(@"METAR EGPH 281050Z VRB02KT CAVOK 18/13 Q1027")]
        [DataRow(@"METAR EGTE 281050Z 03007KT 340V100 9999 FEW016 18/12 Q1027")]
        [DataRow(@"METAR EGAC 281050Z AUTO VRB03KT 9999 R22/0750D -RA NCD 19/12 Q1028")]
        [DataRow(@"METAR EGPF 281050Z AUTO 23006KT 170V280 9999 NCD 18/14 Q1028")]
        [DataRow(@"METAR EGNM 281050Z VRB01KT 9999 SCT022 17/12 Q1028")]
        [DataRow(@"METAR EGGP 281050Z 24005KT 9999 FEW030 18/11 Q1028")]
        [DataRow(@"METAR EGKK 281050Z 01009KT 9999 SCT024 19/12 Q1025")]
        [DataRow(@"METAR EGLL 281050Z AUTO 03008KT 340V060 9999 SCT028 BKN032 19/12 Q1026 NOSIG")]
        [DataRow(@"METAR EGGW 281050Z 01010KT 9999 VCSH BKN015 BKN021 BKN036 16/13 Q1027")]
        [DataRow(@"METAR EGSS 281050Z AUTO 01009KT 330V040 9999 FEW019 SCT032 BKN047 18/13 Q1026")]
        [DataRow(@"METAR EGCC 281050Z AUTO VRB01KT 9999 SCT027 18/11 Q1028 NOSIG")]
        [DataRow(@"METAR EGNT 281050Z 23004KT 190V280 9999 FEW023 18/11 Q1028")]
        [DataRow(@"METAR EGSH 281050Z 02013KT 9999 VCSH FEW020 SCT029 18/14 Q1026 TEMPO SHRA")]
        [DataRow(@"METAR LCRA 281050Z 22009KT 9999 FEW022 30/23 Q1007 NOSIG")]
        [DataRow(@"METAR EGUL 281056Z AUTO 01015G20KT 9999 FEW027 BKN065 19/12 A3028 RMK AO2 SLP257 T01880116 $")]
        [DataRow(@"METAR EGCN 281050Z 36004KT 310V030 9999 FEW027 SCT035 19/12 Q1029")]
        [DataRow(@"METAR EGHI 281050Z 01009KT 340V050 9999 FEW020 SCT030 18/11 Q1026")]
        /*
        [DataRow(@"METAR EGVN 281050Z 01008KT 9999 FEW022 BKN024 17/11 Q1027 BECMG SCT025 RMK WHT BECMG")]
        [DataRow(@"METAR EGUN 281056Z 02011KT 9999 FEW043 BKN080 BKN190 19/15 A3029 RMK SLP260 T01880151 RVRNO TSNO $")]
        [DataRow(@"METAR EGVA 281056Z AUTO 36004KT 9999 SCT020 BKN027 17/12 A3032 RMK AO2 CIG 020V027 BKN V OVC SLP270 T01680121 $")]
        */
        public void MetarValidationStandard(string metar)
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = Metarwiz.Parse(metar);
            
            /* assert */
            Console.WriteLine(m.Metar.ToString(true));
            Console.WriteLine(m.ToString());

            Assert.AreEqual(m.Metar.ToString(true), m.ToString());
        }

        [TestMethod]
        [DataRow(@"METAR KDEN 281153Z 01006KT 10SM FEW180 FEW220 17/06 A3002 RMK AO2 SLP075 T01720061 10250 20172 53014")]
        [DataRow(@"METAR KORD 281151Z 20006KT 10SM FEW040 FEW070 SCT250 24/22 A3008 RMK AO2 SLP181 T02440217 10272 20244 53012")]
        /*
        [DataRow(@"METAR KATL 281152Z 11004KT 1SM R09R/P6000FT BR BKN004 24/23 A3020 RMK AO2 SFC VIS 4 SLP215 VIS N-E 1 1/2 CIG 003V005 TWRINC T02390228 10250 20239 53012")]
        [DataRow(@"METAR KSEA 281153Z 05006KT 10SM CLR 13/10 A3014 RMK AO2 SLP212 70002 T01330100 10156 20133 55002")]
        [DataRow(@"METAR KLAX 281153Z 31005KT 1/4SM R25L/4500VP6000FT FG OVC002 16/16 A2974 RMK AO2 SLP068 T01610156 10172 20161 55001")]
        [DataRow(@"METAR KJFK 281151Z 07015KT 10SM SCT013 SCT020 BKN100 21/19 A3021 RMK AO2 RAE51 SLP230 P0008 60084 70087 T02110194 10261 20206 51012 $")]
        [DataRow(@"METAR KSFO 281156Z 28005KT 10SM CLR 16/12 A2969 RMK AO2 SLP054 T01610122 10183 20161 53002 $")]
        [DataRow(@"METAR KLAS 281156Z 00000KT 10SM CLR 28/M02 A2977 RMK AO2 SLP049 T02831022 10339 20283 51007")]
        [DataRow(@"METAR CYYZ 281200Z 07004KT 15SM FEW019 SCT140 BKN220 20/14 A3019 RMK SC1AC3CC1 SLP222 DENSITY ALT 1000FT")]
        */
        public void MetarValidationNorthAmerican(string metar)
        {
            /* arrange */
            Metarwiz m;

            /* act */
            m = Metarwiz.Parse(metar);

            /* assert */
            Console.WriteLine(m.Metar.ToString(true));
            Console.WriteLine(m.ToString());

            Assert.AreEqual(m.Metar.ToString(true), m.ToString());
        }
        
        [TestMethod]
        [DataRow(@"")]
        [DataRow(@"MATER")]
        [DataRow(@"RMK")]
        [DataRow(@"=")]
        [DataRow(null)]
        public void MetarValidationExceptions(string metar)
        {
            /* arrange */
            Action action;

            /* act */
            action = () => { Metarwiz.Parse(metar); };

            /* assert */
            Assert.ThrowsException<Exception>(action);
        }
    }
}