using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser
{
    public static class MetarParserFactory
    {
        public static BaseMetarItem Create(Type t, Match m)
        {
            return (BaseMetarItem)Activator.CreateInstance(t, new object[] { m });
        }
    }
}
