using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwStateOfRunway : BaseMetarItem
    {
        private readonly string _prefix;
        private readonly int _runway;
        private readonly string _designator;
        private readonly string _separator;
        private readonly string _code;
        private readonly string _deposit;
        private readonly string _extent;
        private readonly string _depth;
        private readonly string _friction;

        public MwStateOfRunway(Match match)
        {
            _prefix = match.Groups["PREFIX"].Value;
            _ = int.TryParse(match.Groups["RUNWAY"].Value, out _runway);
            _designator = match.Groups["DESIGNATOR"].Value;
            _separator = match.Groups["SEPARATOR"].Value;
            _code = match.Groups["CODE"].Value;
            _deposit = _code.Substring(0, 1);
            _extent = _code.Substring(1, 1);
            _depth = _code.Substring(2, 2);
            _friction = _code.Substring(4, 2);
        }

        public string Deposit => _deposit;
        public string DepositDescription => _deposit switch
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

        public string Extent => _extent;
        public string ExtentDescription => _extent switch
            {
                "1" => "10% or less",
                "2" => "11% to 25%",
                "5" => "26% to 50%",
                "9" => "51% to 100%",
                _ => "Not Reported"
            };

        public string Depth => _depth;
        public int DepthValue
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
        public string Friction => _friction;
        public bool ClosedDueToSnow => _code == "SNOCLO";
        public bool Cleared => _code.Substring(0, 4) == "CLRD";
        public bool Operational => !(_depth == "99");
        public bool IsNoNewInformation => _runway == 99;
        public bool IsAllRunways => _runway == 88;
        public bool IsNoSpecificRunway => (IsNoNewInformation || IsAllRunways);
        public bool IsLeft => (!IsNoSpecificRunway) && (_runway < 50);
        public bool IsRight => (!IsNoSpecificRunway) && (_runway > 50);
        public string Orientation => (!IsNoSpecificRunway) ? (IsLeft) ? String.Empty : "R" : String.Empty;
        public int Bearing => (!IsNoSpecificRunway) ? (IsLeft) ? _runway : (_runway - 50) : 0;
        public string Runway => (!IsNoSpecificRunway) ? $"{Bearing}{Orientation}" : String.Empty;

        public static string Pattern => @"( )(?<PREFIX>R)(?<RUNWAY>\d{2})?(?<DESIGNATOR>L|C|R)?(?<SEPARATOR>/)(?<CODE>\S{6}(\s|\b|$))";

        public override string ToString()
        {
            return String.Concat(
                _prefix,
                (_runway > 0) ? _runway.ToString("D2") : String.Empty,
                _designator,
                _separator,
                _deposit,
                _extent,
                _depth,
                _friction
            );
        }
    }
}
