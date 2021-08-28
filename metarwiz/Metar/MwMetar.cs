using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwMetar : MwMetarItem
    {
        private readonly string _metar;

        public MwMetar(int position, string value) : base(position, value, Pattern)
        {
            _metar = Groups["METAR"].Value;
        }

        public static string Pattern => @"^(?<METAR>METAR)$";

        public static bool IsMatch(int position, string value) => position == 1 && Match(value, Pattern);

        public override string ToString()
        {
            return _metar;
        }
    }
}
