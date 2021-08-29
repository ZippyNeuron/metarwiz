namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwRemarks : RwMetarItem
    {
        private readonly string _rmk;

        public RwRemarks(int position, string value) : base(position, value, Pattern)
        {
            _rmk = Groups["RMK"].Value;
        }

        public static string Pattern => @"^(?<RMK>RMK)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return _rmk;
        }
    }
}
