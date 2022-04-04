using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwSurfaceWindGroup : BaseMetarItem
    {
        private readonly int _direction;
        private readonly int _gusting;
        private readonly int _speed;
        private readonly string _units;
        private readonly string _vrb;
        private readonly bool _sexceeds100;
        private readonly bool _gexceeds100;
        private readonly string _v;
        private readonly int _from;
        private readonly int _to;
        private readonly string _g;

        public MwSurfaceWindGroup(Match match)
        {
            _vrb = match.Groups["VRB"].Value; ;
            _ = int.TryParse(match.Groups["DIRECTION"].Value, out _direction);
            _ = int.TryParse(match.Groups["GUSTING"].Value, out _gusting);
            _ = int.TryParse(match.Groups["SPEED"].Value, out _speed);
            _sexceeds100 = !String.IsNullOrEmpty(match.Groups["SEXCEEDS100"].Value);
            _gexceeds100 = !String.IsNullOrEmpty(match.Groups["GEXCEEDS100"].Value);
            _units = match.Groups["UNITS"].Value;
            _ = int.TryParse(match.Groups["FROM"].Value, out _from);
            _ = int.TryParse(match.Groups["TO"].Value, out _to);
            _v = match.Groups["V"].Value;
            _g = match.Groups["G"].Value;
        }

        public int Direction => _direction;

        public decimal Gusting => _gusting;

        public int Speed => _speed;

        public bool SpeedExceeds100 => _sexceeds100;

        public bool GustingExceeds100 => _gexceeds100;

        public bool HasDirectionalVariations => !String.IsNullOrEmpty(_v);

        public bool IsGusting => !String.IsNullOrEmpty(_g);

        public SpeedUnit Units => _units switch
        {
            "MPS" => SpeedUnit.MPS,
            "KT" => SpeedUnit.KT,
            _ => SpeedUnit.Unspecified
        };

        public int From => _from;

        public int To => _to;

        public string UnitsDescription => Units.GetDescription();

        public bool IsVariable => _vrb == "VRB";

        public static string Pattern => @"( )((?<VRB>VRB)|(?<DIRECTION>\d{3}))((?<SEXCEEDS100>P)?(?<SPEED>(\d{2,3})))?((?<G>G)(?<GEXCEEDS100>P)?(?<GUSTING>(\d{2,3})))?(?<UNITS>MPS|KT)(\ )?((?<FROM>\d{3})(?<V>V)(?<TO>\d{3}))?";

        public override string ToString()
        {
            return String.Concat(
                (IsVariable) ? _vrb : _direction.ToString("D3"),
                _speed.ToString("D2"),
                (IsGusting) ? $"{_g}{_gusting.ToString("D")}" : String.Empty,
                Enum.GetName<SpeedUnit>(Units),
                (HasDirectionalVariations) ? $" {_from.ToString("D3")}{_v}{_to.ToString("D3")}" : String.Empty
            );
        }
    }
}
