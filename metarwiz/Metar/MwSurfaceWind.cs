using System;
using ZippyNeuron.Metarwiz.Abstractions;
using ZippyNeuron.Metarwiz.Enums;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwSurfaceWind : MvMetarItem
    {
        private readonly int _direction;
        private readonly int _gusting;
        private readonly int _speed;
        private readonly string _units;
        private readonly string _vrb;

        private const decimal _MpsToKts = 1.943844m;
        private const decimal _KtsToMps = 0.514444m;

        public MwSurfaceWind(int position, string value) : base(position, value, Pattern)
        {
            _vrb = Groups["VRB"].Value; ;
            _ = int.TryParse(Groups["DIRECTION"].Value, out _direction);
            _ = int.TryParse(Groups["GUSTING"].Value, out _gusting);
            _ = int.TryParse(Groups["SPEED"].Value, out _speed);
            _units = Groups["UNITS"].Value;
        }

        /* the incoming report will specify this in degrees magnetic */
        public int Direction => _direction;

        /* speeds are based on an averaging period of ten minutes */
        public decimal Gusting => _gusting;

        /* speeds are based on an averaging period of ten minutes */
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
