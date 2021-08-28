using System.Collections.Generic;
using ZippyNeuron.Metarwiz.Abstractions;
using ZippyNeuron.Metarwiz.Utilities;

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
