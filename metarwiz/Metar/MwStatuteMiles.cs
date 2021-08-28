using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwStatuteMiles : MwMetarItem
    {
        private readonly string _sm;
        private readonly int _distance;

        public MwStatuteMiles(int position, string value) : base(position, value, Pattern)
        {
            _sm = Groups["SM"].Value;
            _ = int.TryParse(Groups["DISTANCE"].Value, out _distance);
        }

        [Obsolete("This property will be removed with next release")]
        public bool IsSM => _sm == "SM";

        [Obsolete("This property will be removed with next release, please use Distance")]
        public string Amount => String.Format("{0:0000}", Distance);

        public int Distance => _distance;

        public static string Pattern => @"^(?<DISTANCE>\S+)(?<SM>SM)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                String.Format("{0:00}", Distance),
                _sm
            );
        }
    }
}
