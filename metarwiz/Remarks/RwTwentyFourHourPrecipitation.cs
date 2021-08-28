using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Remarks
{
    public class RwTwentyFourHourPrecipitation : RwMetarItem
    {
        private readonly string _seven;
        private readonly decimal _units = 0.01m;
        private readonly int _amount;

        public RwTwentyFourHourPrecipitation(int position, string value) : base(position, value, Pattern)
        {
            _seven = Groups["7"].Value;
            _ = int.TryParse(Groups["AMOUNT"].Value, out _amount);
        }

        public bool IsTrace => _amount == 0;

        public decimal Inches => _amount * _units;

        public static string Pattern => @"^(?<7>7)(?<AMOUNT>\d{4})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _seven,
                String.Format("{0:0000}", _amount)
            );
        }
    }
}
