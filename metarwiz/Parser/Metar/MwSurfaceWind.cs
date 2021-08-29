using System;
using ZippyNeuron.Metarwiz.Enums;

namespace ZippyNeuron.Metarwiz.Parser.Metar
{
    public class MwSurfaceWind : MwMetarItem
    {
        private readonly int _direction;
        private readonly int _gusting;
        private readonly int _speed;
        private readonly string _units;
        private readonly string _vrb;

        public MwSurfaceWind(int position, string value) : base(position, value, Pattern)
        {
            _vrb = Groups["VRB"].Value; ;
            _ = int.TryParse(Groups["DIRECTION"].Value, out _direction);
            _ = int.TryParse(Groups["GUSTING"].Value, out _gusting);
            _ = int.TryParse(Groups["SPEED"].Value, out _speed);
            _units = Groups["UNITS"].Value;
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

        public bool IsVariable => _vrb == "VRB";

        public static string Pattern => @"^((?<VRB>VRB)|(?<DIRECTION>\d{3}))(?<SPEED>\d{2})?(G(?<GUSTING>\d{2}))?(?<UNITS>MPS|KT)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

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
