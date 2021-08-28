using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Remarks
{
    public class RwAutomatedStation : RwMetarItem
    {
        private readonly string _a;
        private readonly int _rainsnowsensor;

        public RwAutomatedStation(int position, string value) : base(position, value, Pattern)
        {
            _a = Groups["A"].Value;
            _ = int.TryParse(Groups["RAINSNOWSENSOR"].Value, out _rainsnowsensor);
        }

        public bool HasRainSnowSensor => _rainsnowsensor == 2;

        public static string Pattern => @"^(?<A>AO)(?<RAINSNOWSENSOR>\d{1})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _a, 
                String.Format("{0:0}", _rainsnowsensor)
            );
        }
    }
}
