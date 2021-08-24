using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwVacinityThunderstorm : MvMetarItem
    {
        private readonly string _vcts;

        public MwVacinityThunderstorm(int position, string value) : base(position, value, Pattern)
        {
            _vcts = Groups["VCTS"].Value;
        }

        public bool IsVCTS => _vcts == "VCTS";

        public static string Pattern => @"^(?<VCTS>VCTS)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return _vcts;
        }
    }
}
