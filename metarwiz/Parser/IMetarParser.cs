using System.Collections.Generic;

namespace ZippyNeuron.Metarwiz.Parser
{
    internal interface IMetarParser
    {
        IEnumerable<MetarParserItem> Items { get; }

        IDictionary<int, IMetarItem> Parse();

        MetarInfo MetarInfo { get; }
    }
}