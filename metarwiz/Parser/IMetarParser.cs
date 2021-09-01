using System.Collections.Generic;

namespace ZippyNeuron.Metarwiz.Parser
{
    internal interface IMetarParser
    {
        IEnumerable<MetarParserItem> Items { get; }

        IEnumerable<IMetarItem> Parse();

        MetarInfo MetarInfo { get; }
    }
}