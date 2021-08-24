using System.ComponentModel;

namespace ZippyNeuron.Metarwiz.Enums
{
    public enum ObservationType
    {
        [Description("Unspecified")]
        U,
        [Description("Below The Minimum Value")]
        M,
        [Description("Greater Than Maximum Value")]
        P
    }
}
