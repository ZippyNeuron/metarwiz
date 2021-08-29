using System.Collections.Generic;
using ZippyNeuron.Metarwiz.Parser;

namespace ZippyNeuron.Metarwiz
{
    public interface IMetarwiz
    {
        MetarInfo Metar { get; }

        T Get<T>() where T : IMetarItem;

        IEnumerable<T> GetMany<T>() where T : IMetarItem;
        
        string ToString();
    }
}
