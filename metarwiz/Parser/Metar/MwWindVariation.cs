using System;

namespace ZippyNeuron.Metarwiz.Parser.Metar
{
    public class MwWindVariation : MwMetarItem
    {
        private readonly int _from;
        private readonly int _to;

        public MwWindVariation(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["FROM"].Value, out _from);
            _ = int.TryParse(Groups["TO"].Value, out _to);
        }

        public int From => _from;

        public int To => _to;

        public static string Pattern => @"^(?<FROM>\d{3})V(?<TO>\d{3})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                String.Format("{0:000}", From),
                @"V",
                String.Format("{0:000}", To)
            );
        }
    }
}
