using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Factories
{
    public interface IMetarItemFactory
    {
        IMetarItem Create(int position, string item);
    }
}
