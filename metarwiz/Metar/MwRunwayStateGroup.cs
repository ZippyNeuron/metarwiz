using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwRunwayStateGroup : MwMetarItem
    {
        private readonly int _runway;
        private readonly string _extent;
        private readonly string _deposit;
        private readonly string _depth;

        public MwRunwayStateGroup(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["RUNWAY"].Value, out _runway);
            _extent = Groups["EXTENT"].Value;
            _deposit = Groups["DEPOSIT"].Value;
            _depth = Groups["DEPTH"].Value; 
        }

        public string Deposit => _deposit switch
            {
                "0" => "Clear and Dry",
                "1" => "Damp",
                "2" => "Wet or Water Patches",
                "3" => "Rime or Frost",
                "4" => "Dry Snow",
                "5" => "Wet Snow",
                "6" => "Slush",
                "7" => "Ice",
                "8" => "Compacted or Rolled Snow",
                "9" => "Frozen Ruts or Ridges",
                _ => "Not Reported"
            };

        public string Extent => _extent switch
            {
                "1" => "10% or less",
                "2" => "11% to 25%",
                "5" => "26% to 50%",
                "9" => "51% to 100%",
                _ => "Not Reported"
            };

        public bool Operational => !(_depth == "99");

        public int Depth
        {
            get {
                if (int.TryParse(_depth, out int depth))
                {
                    return depth switch
                    {
                        <= 90 => depth,
                        91 => 0,
                        >= 92 => (depth - 90) * 50
                    };
                } else {
                    return 0;
                }
            }
        }

        public bool IsNoNewInformation => _runway == 99;

        public bool IsAllRunways => _runway == 88;

        public bool IsNoSpecificRunway => (IsNoNewInformation || IsAllRunways);

        public bool IsLeft => (!IsNoSpecificRunway) && (_runway < 50);

        public bool IsRight => (!IsNoSpecificRunway) && (_runway > 50);

        public string Orientation => (!IsNoSpecificRunway) ? (IsLeft) ? String.Empty : "R" : String.Empty;

        public int Bearing => (!IsNoSpecificRunway) ? (IsLeft) ? _runway : (_runway - 50) : 0;

        public string Runway => (!IsNoSpecificRunway) ? $"{Bearing}{Orientation}" : String.Empty;

        public static string Pattern => @"^(?<RUNWAY>\d{2})(?<DEPOSIT>\d{1}|\/)(?<EXTENT>\d{1})\/\/(?<DEPTH>\d{2})$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return String.Concat(
                String.Format("{0:00}", _runway),
                _deposit,
                _extent,
                @"//",
                _depth
            );
        }
    }
}
