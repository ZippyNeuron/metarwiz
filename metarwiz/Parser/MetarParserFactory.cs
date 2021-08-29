using System;
using System.Collections.Generic;
using ZippyNeuron.Metarwiz.Parser.Metar;
using ZippyNeuron.Metarwiz.Parser.Remarks;

namespace ZippyNeuron.Metarwiz.Parser
{
    public class MetarParserFactory : MetarFactory, IMetarParserFactory
    {
        internal MetarParserFactory() { }

        public IMetarItem Create(MetarParserItem item)
        {
            Type type = (item.Type == MetarParserItemType.Metar) ? typeof(MwMetarItem) : typeof(RwMetarItem);

            IEnumerable<Type> types = GetTypesOfBase(type);

            foreach (Type t in types)
            {
                if (IsMatch(t, item.Position, item.Value))
                {
                    return CreateMetarItem(t, item.Position, item.Value);
                }
            }

            return null;
        }
    }
}
