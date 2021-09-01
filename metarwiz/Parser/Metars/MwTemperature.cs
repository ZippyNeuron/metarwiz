using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwTemperature : BaseMetarItem
    {
        private readonly string _tempSign;
        private readonly int _temperature;
        private readonly string _dewPointSign;
        private readonly int _dewPoint;

        public MwTemperature(Match match)
        {
            _ = int.TryParse(match.Groups["TEMPERATURE"].Value, out _temperature);
            _ = int.TryParse(match.Groups["DEWPOINT"].Value, out _dewPoint);
            _tempSign = match.Groups["TEMPERATURESIGN"].Value;
            _dewPointSign = match.Groups["DEWPOINTSIGN"].Value;
        }

        public int Celsius => (_tempSign == "M") ? _temperature * -1 : _temperature;

        public int DewPoint => (_dewPointSign == "M") ? _dewPoint * -1 : _dewPoint;
        
        public static string Pattern => @"\ (?<TEMPERATURESIGN>M|)(?<TEMPERATURE>\d+)\/(?<DEWPOINTSIGN>M|)(?<DEWPOINT>\d+)";

        public override string ToString()
        {
            return String.Concat(
                _tempSign,
                String.Format("{0:00}", Math.Abs(Celsius)),
                @"/",
                _dewPointSign,
                String.Format("{0:00}", Math.Abs(DewPoint))
            );
        }
    }
}
