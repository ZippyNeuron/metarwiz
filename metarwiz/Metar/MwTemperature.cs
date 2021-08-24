using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwTemperature : MvMetarItem
    {
        private readonly string _tempSign;
        private readonly int _temperature;
        private readonly string _dewPointSign;
        private readonly int _dewPoint;

        public MwTemperature(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["TEMPERATURE"].Value, out _temperature);
            _ = int.TryParse(Groups["DEWPOINT"].Value, out _dewPoint);
            _tempSign = Groups["TEMPERATURESIGN"].Value;
            _dewPointSign = Groups["DEWPOINTSIGN"].Value;
        }

        public int Celsius => (_tempSign == "M") ? _temperature * -1 : _temperature;

        public int DewPoint => (_dewPointSign == "M") ? _dewPoint * -1 : _dewPoint;
        
        public static string Pattern => @"^(?<TEMPERATURESIGN>M|)(?<TEMPERATURE>\d+)\/(?<DEWPOINTSIGN>M|)(?<DEWPOINT>\d+)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _tempSign,
                String.Format("{0:00}", _temperature),
                @"/",
                _dewPointSign,
                String.Format("{0:00}", _dewPoint)
            );
        }
    }
}
