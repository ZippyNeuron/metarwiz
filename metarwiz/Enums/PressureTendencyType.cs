using System.ComponentModel;

namespace ZippyNeuron.Metarwiz.Enums
{
    public enum PressureTendencyType
    {
        [Description("Increasing Then Decreasing")]
        IncreasingThenDecreasing = 0,
        [Description("Increasing Then Steady")]
        IncreasingThenSteady = 1,
        [Description("Increasing Steadily or Unsteadily")]
        IncreasingSteadilyOrUnsteadily = 2,
        [Description("Decreasing or Steady")]
        DecreasingOrSteady = 3,
        [Description("Steady")]
        Steady = 4,
        [Description("Decreasing Then Increasing")]
        DecreasingThenIncreasing = 5,
        [Description("Decreasing Then Steady")]
        DecreasingThenSteady = 6,
        [Description("Decreasing Steadily or Unsteadily")]
        DecreasingSteadilyOrUnsteadily = 7,
        [Description("Steady or Increasing")]
        SteadyOrIncreasing = 8
    }
}
