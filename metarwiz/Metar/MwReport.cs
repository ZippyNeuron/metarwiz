using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwReport : MvMetarItem
    {
        private readonly string _type;

        public MwReport(int position, string value) : base(position, value, Pattern)
        {
            _type = Groups["METAR"].Value;
        }

        public string Type => _type;

        public static string Pattern => @"^(?<METAR>METAR)$";

        public static bool IsMatch(int position, string value) => position == 0 && Match(value, Pattern);

        public override string ToString()
        {
            return _type;
        }
    }
}
