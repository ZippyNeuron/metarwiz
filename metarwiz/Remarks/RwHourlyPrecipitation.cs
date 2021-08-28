using System;
using ZippyNeuron.Metarwiz.Abstractions;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Remarks
{
    public class RwHourlyPrecipitation : RwMetarItem
    {
        private readonly string _p;
        private readonly decimal _units = 0.01m;
        private readonly int _amount;

        public RwHourlyPrecipitation(int position, string value) : base(position, value, Pattern)
        {
            _p = Groups["P"].Value;
            _ = int.TryParse(Groups["AMOUNT"].Value, out _amount);
        }

        public bool IsTrace => _amount == 0;

        public decimal Inches => _amount * _units;

        public static string Pattern => @"^(?<P>P)(?<AMOUNT>\d{4})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(_p, String.Format("{0:0000}", _amount));
        }
    }
}
