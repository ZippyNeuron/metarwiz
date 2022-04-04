using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Utilities;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwVisibilityGroup : BaseMetarItem
    {
        /* icao */
        private readonly int _minimumVisibility;
        private readonly int _directionVisibility;
        private readonly string _direction;
        private readonly string _cavok;
        private readonly bool _isCAVOK;

        /* north american shenanigans */
        private readonly decimal _part1;
        private readonly decimal _part2;
        private readonly string _over;
        private readonly int _statuteMiles;
        private readonly string _sm;
        private readonly bool _isFraction;
        private readonly bool _isSM; 

        public MwVisibilityGroup(Match match)
        {
            _ = int.TryParse(match.Groups["MINVISIBILITY"].Value, out _minimumVisibility);
            _ = int.TryParse(match.Groups["DIRVISIBILITY"].Value, out _directionVisibility);
            _cavok = match.Groups["CAVOK"].Value;
            _isCAVOK = !String.IsNullOrEmpty(_cavok);
            _direction = match.Groups["DIRECTION"].Value;
            _ = decimal.TryParse(match.Groups["PT1"].Value, out _part1);
            _over = match.Groups["OVER"].Value;
            _ = decimal.TryParse(match.Groups["PT2"].Value, out _part2);
            _ = int.TryParse(match.Groups["STATUTEMILES"].Value, out _statuteMiles);
            _sm = match.Groups["SM"].Value;
            _isSM = _sm.Equals("SM");
            _isFraction = _part1 > 0 && _part2 > 0 && _over.Equals("/");
            
            if (_isCAVOK) {
                _minimumVisibility = 0;
            } else if (_isSM) {
                if (_isFraction) {
                    _minimumVisibility = Convert.ToInt32(MetarConvert.MetersPerMile * (_part1 / _part2));
                } else {
                    _minimumVisibility = Convert.ToInt32(_statuteMiles * MetarConvert.MetersPerMile);
                }
            }
        }

        public bool IsCAVOK => _isCAVOK;
        public int MinimumVisibility => _minimumVisibility;
        public bool IsMinimumVisibilityMoreThan10K => _minimumVisibility >= 9999;
        public string Direction => _direction;
        public int DirectionVisibility => _directionVisibility;
        public bool HasDirectionVisibility => !String.IsNullOrEmpty(_direction);

        public static string Pattern => @"( )(?<CAVOK>CAVOK)|(?<MINVISIBILITY>\ \d{4}(?=\ |$))(\ (?<DIRVISIBILITY>\d{4})(?<DIRECTION>\w*))?|((?<PT1>\d+)(?<OVER>\/)(?<PT2>\d+)|(?<STATUTEMILES>\d+))(?<SM>SM)";

        public override string ToString()
        {
            string s;
            
            if (_isCAVOK) {
                s = _cavok;
            } else if (_isSM) {
                if (_isFraction) {
                    s = _isFraction ? $"{_part1}{_over}{_part2}{_sm}"  : $"{_statuteMiles}{_sm}";
                } else {
                    s = $"{_statuteMiles.ToString()}{_sm}";
                }
            } else {
                s = String.Concat(
                    _minimumVisibility.ToString("D4"),
                    (HasDirectionVisibility) ? $" {_directionVisibility.ToString("D4")}{_direction}" : String.Empty
                );
            }

            return s;
        }
    }
}
