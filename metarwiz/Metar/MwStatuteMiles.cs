using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwStatuteMiles : MvMetarItem
    {
        private readonly string _sm;
        private readonly string _amount;

        public MwStatuteMiles(int position, string value) : base(position, value, Pattern)
        {
            _sm = Groups["SM"].Value;
            _amount = Groups["AMOUNT"].Value;
        }

        public bool IsSM => _sm == "SM";

        public string Amount => _amount;

        public static string Pattern => @"^(?<AMOUNT>\S+)(?<SM>SM)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(Amount, _sm);
        }
    }
}
