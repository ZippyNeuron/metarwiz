using System.ComponentModel;

namespace ZippyNeuron.Metarwiz.Enums
{
    public enum CloudType
    {
        [Description("Unspecified")]
        Unspecified,
        [Description("Broken")]
        BKN,
        [Description("Scattered")]
        SCT,
        [Description("Few")]
        FEW,
        [Description("Overcast")]
        OVC,
        [Description("No Clouds Detected")]
        NCD,
    }
}
