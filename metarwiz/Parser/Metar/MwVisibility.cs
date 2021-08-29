using System;

namespace ZippyNeuron.Metarwiz.Parser.Metar
{
    public class MwVisibility : MwMetarItem
    {
        private readonly int _distance;

        public MwVisibility(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["DISTANCE"].Value, out _distance);
        }

        public int Distance => _distance;

        public bool IsMoreThanTenKilometres => _distance >= 9999;

        public static string Pattern => @"^(?<DISTANCE>\d{4})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Format("{0:0000}", Distance);
        }
    }
}
