using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwVisibility : MvMetarItem
    {
        private readonly int _distance;

        public MwVisibility(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["DISTANCE"].Value, out _distance);
        }

        public int Visibility => _distance;

        public bool IsMoreThanTenKilometres => _distance == 9999;

        public static string Pattern => @"^(?<DISTANCE>\d{4})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Format("{0:0000}", Visibility);
        }
    }
}
