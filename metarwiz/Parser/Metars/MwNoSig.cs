namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwNoSig : MwMetarItem
    {
        private readonly string _nosig;

        public MwNoSig(int position, string value) : base(position, value, Pattern)
        {
            _nosig = Groups["NOSIG"].Value;
        }

        public static string Pattern => @"^(?<NOSIG>NOSIG)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return _nosig;
        }
    }
}
