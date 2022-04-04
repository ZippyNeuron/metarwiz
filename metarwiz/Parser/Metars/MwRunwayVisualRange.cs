using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwRunwayVisualRange : BaseMetarItem
    {
        private readonly int _runway;
        private readonly string _designator;
        private readonly string _observation;
        private readonly int _range;
        private readonly string _tendency;
        private readonly string _v;
        private readonly int _to;
        private readonly string _r;
        private readonly string _divider;

        public MwRunwayVisualRange(Match match)
        {
            _designator = match.Groups["DESIGNATOR"].Value;
            _observation = match.Groups["OBSERVATION"].Value;
            _tendency = match.Groups["TENDENCY"].Value;
            _ = int.TryParse(match.Groups["RUNWAY"].Value, out _runway);
            _ = int.TryParse(match.Groups["RANGE"].Value, out _range);
            _v = match.Groups["V"].Value;
            _ = int.TryParse(match.Groups["TO"].Value, out _to);
            _r = match.Groups["R"].Value;
            _divider = match.Groups["DIVIDER"].Value;
        }

        public int Runway => _runway;
        public RunwayType Designator => _designator switch
            {
                "L" => RunwayType.L,
                "C" => RunwayType.C,
                "R" => RunwayType.R,
                _ => RunwayType.U
            };
        public string DesignatorDescription => Designator.GetDescription();
        public int Range => _range;
        public int To => _to;
        public ObservationType Observation => _observation switch
            {
                "P" => ObservationType.P,
                "M" => ObservationType.M,
                _ => ObservationType.U
            };
        public string ObservationDescription => Observation.GetDescription();
        public TendencyIndicator Tendency => _tendency switch
            {
                "U" => TendencyIndicator.U,
                "N" => TendencyIndicator.N,
                "D" => TendencyIndicator.D,
                "FT" => TendencyIndicator.FT,
                _ => TendencyIndicator.Unspecified
            };
        public string TendencyDescription => Tendency.GetDescription();

        public static string Pattern => @"( )(?<R>R)(?<RUNWAY>\d{2})(?<DESIGNATOR>L|R|C)?(?<DIVIDER>\/)((?<OBSERVATION>P|M)?(?<RANGE>\d{4}(?=V|U|D|N|\b))((?<V>V)(?<TO>\d{4}))?)(?<TENDENCY>U|D|N)?";
        
        public override string ToString()
        {
            return String.Concat(
                _r,
                Runway.ToString("D2"),
                (Designator != RunwayType.U) ? Enum.GetName<RunwayType>(Designator) : String.Empty,
                _divider,
                (Observation != ObservationType.U) ? Enum.GetName<ObservationType>(Observation) : String.Empty,
                Range.ToString("D4"),
                (!String.IsNullOrEmpty(_v) ? $"{_v}{_to.ToString("D4")}" : String.Empty),
                (Tendency != TendencyIndicator.Unspecified) ? Enum.GetName<TendencyIndicator>(Tendency) : String.Empty
            );
        }
    }
}
