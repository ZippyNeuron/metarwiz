using System.ComponentModel;

namespace ZippyNeuron.Metarwiz.Enums
{
    public enum TornadicType
    {
        [Description("Unspecified")]
        Unspecified = 0,
        [Description("Tornado")]
        TORNADO = 1,
        [Description("Funnel Cloud")]
        FUNNEL_CLOUD = 2,
        [Description("Waterspout")]
        WATERSPOUT = 3
    }
}
