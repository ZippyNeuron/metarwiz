using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwSurfaceWind : BaseMetarItem
    {
        private readonly int _direction;
        private readonly int _gusting;
        private readonly int _speed;
        private readonly string _units;
        private readonly string _vrb;

        public MwSurfaceWind(Match match)
        {
            _vrb = match.Groups["VRB"].Value; ;
            _ = int.TryParse(match.Groups["DIRECTION"].Value, out _direction);
            _ = int.TryParse(match.Groups["GUSTING"].Value, out _gusting);
            _ = int.TryParse(match.Groups["SPEED"].Value, out _speed);
            _units = match.Groups["UNITS"].Value;
        }

        public int Direction => _direction;

        public decimal Gusting => _gusting;

        public int Speed => _speed;

        public SpeedUnit Units => _units switch
        {
            "MPS" => SpeedUnit.MPS,
            "KT" => SpeedUnit.KT,
            _ => SpeedUnit.Unspecified
        };

        public string UnitsDescription => Units.GetDescription();

        public bool IsVariable => _vrb == "VRB";

        public static string Pattern => @"\ ((?<VRB>VRB)|(?<DIRECTION>\d{3}))(?<SPEED>\d{2})?(G(?<GUSTING>\d{2}))?(?<UNITS>MPS|KT)";

        public override string ToString()
        {
            return String.Concat(
                (IsVariable) ? _vrb : String.Format("{0:000}", Direction),
                String.Format("{0:00}", Speed),
                (Gusting) > 0 ? String.Format("G{0:00}", Gusting) : String.Empty,
                Enum.GetName<SpeedUnit>(Units)
            );
        }
    }
}
