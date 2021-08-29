namespace ZippyNeuron.Metarwiz.Parser.Metar
{
    public class MwCavok : MwMetarItem
    {
        private readonly string _cavok;

        public MwCavok(int position, string value) : base(position, value, Pattern)
        {
            _cavok = Groups["CAVOK"].Value;
        }

        public static string Pattern => @"^(?<CAVOK>CAVOK)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return _cavok;
        }
    }
}
