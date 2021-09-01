using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwAutomatedStation : BaseMetarItem
    {
        private readonly string _a;
        private readonly int _rainsnowsensor;

        public RwAutomatedStation(Match match)
        {
            _a = match.Groups["A"].Value;
            _ = int.TryParse(match.Groups["RAINSNOWSENSOR"].Value, out _rainsnowsensor);
        }

        public bool HasRainSnowSensor => _rainsnowsensor == 2;

        public static string Pattern => @"\ (?<A>AO)(?<RAINSNOWSENSOR>\d{1})";

        public override string ToString()
        {
            return String.Concat(
                _a, 
                String.Format("{0:0}", _rainsnowsensor)
            );
        }
    }
}
