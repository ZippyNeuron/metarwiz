using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Remarks
{
    public class RwNeedsMaintenance : RwMetarItem
    {
        private readonly string _maintenance;

        public RwNeedsMaintenance(int position, string value) : base(position, value, Pattern)
        {
            _maintenance = Groups["MAINTENANCE"].Value;
        }

        public static string Pattern => @"^(?<MAINTENANCE>\$)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return _maintenance;
        }
    }
}
