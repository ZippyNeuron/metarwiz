using System;

namespace ZippyNeuron.Metarwiz.Parser.Remarks
{
    public class RwVariableCeiling : RwMetarItem
    {
        private const int _multiplier = 100;
        private readonly string _cig;
        private readonly int _from;
        private readonly int _to;

        public RwVariableCeiling(int position, string value) : base(position, value, Pattern)
        {
            _cig = Groups["CIG"].Value;
            _ = int.TryParse(Groups["FROM"].Value, out _from);
            _ = int.TryParse(Groups["TO"].Value, out _to);
        }

        public int From => _from * _multiplier;

        public int To => _to * _multiplier;

        public static string Pattern => @"^(?<CIG>CIG)\ (?<FROM>\d{3})V(?<TO>\d{3})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                _cig,
                " ",
                String.Format("{0:000}", _from),
                @"V",
                String.Format("{0:000}", _to)
            );
        }
    }
}
