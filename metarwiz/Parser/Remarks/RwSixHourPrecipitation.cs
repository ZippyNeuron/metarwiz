using System;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwSixHourPrecipitation : RwMetarItem
    {
        private readonly string _six;
        private readonly decimal _units = 0.01m;
        private readonly int _amount;

        public RwSixHourPrecipitation(int position, string value) : base(position, value, Pattern)
        {
            _six = Groups["6"].Value;
            _ = int.TryParse(Groups["AMOUNT"].Value, out _amount);
        }

        public bool IsTrace => _amount == 0;

        public decimal Inches => _amount * _units;

        public static string Pattern => @"^(?<6>6)(?<AMOUNT>\d{4})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _six, 
                String.Format("{0:0000}", _amount)
            );
        }
    }
}
