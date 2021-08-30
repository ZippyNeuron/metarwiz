namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwTempo : MwMetarItem
    {
        private readonly string _tempo;

        public MwTempo(int position, string value) : base(position, value, Pattern)
        {
            _tempo = Groups["TEMPO"].Value;
        }

        public static string Pattern => @"^(?<TEMPO>TEMPO)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return _tempo;
        }
    }
}
