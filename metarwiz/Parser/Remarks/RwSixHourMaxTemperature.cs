using System;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwSixHourMaxTemperature : RwMetarItem
    {
        private readonly string _one;
        private readonly decimal _units = 0.10m;
        private readonly int _ma;
        private readonly int _amount;

        public RwSixHourMaxTemperature(int position, string value) : base(position, value, Pattern)
        {
            _one = Groups["1"].Value;
            _ = int.TryParse(Groups["MA"].Value, out _ma);
            _ = int.TryParse(Groups["AMOUNT"].Value, out _amount);
        }

        public decimal Celsius => ((_ma == 1) ? _amount * -1 : _amount) * _units;

        public static string Pattern => @"^(?<1>1)(?<MA>\d{1})(?<AMOUNT>\d{3})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _one,
                String.Format("{0:0}", _ma),
                String.Format("{0:000}", _amount)
            );
        }
    }
}
