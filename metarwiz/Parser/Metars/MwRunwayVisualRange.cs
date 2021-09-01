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

        public MwRunwayVisualRange(Match match)
        {
            _designator = match.Groups["DESIGNATOR"].Value;
            _observation = match.Groups["OBSERVATION"].Value;
            _tendency = match.Groups["TENDENCY"].Value;
            _ = int.TryParse(match.Groups["RUNWAY"].Value, out _runway);
            _ = int.TryParse(match.Groups["RANGE"].Value, out _range);
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

        public static string Pattern => @"\ R(?<RUNWAY>\d+)?(?<DESIGNATOR>[A-Z]{1})?\/?(?<OBSERVATION>P|M)?(?<RANGE>\d+)(?<TENDENCY>U|D|N|FT)";

        public override string ToString()
        {
            return String.Concat(
                @"R",
                String.Format("{0:00}", Runway),
                (Designator != RunwayType.U) ? Enum.GetName<RunwayType>(Designator) : String.Empty,
                @"/",
                (Observation != ObservationType.U) ? Enum.GetName<ObservationType>(Observation) : String.Empty,
                String.Format("{0:0000}", Range),
                (Tendency != TendencyIndicator.Unspecified) ? Enum.GetName<TendencyIndicator>(Tendency) : String.Empty
            );
        }
    }
}
