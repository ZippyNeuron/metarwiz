namespace ZippyNeuron.Metarwiz.Abstractions
{
    public abstract class RwMetarItem : BaseMetarItem, IMetarItem
    {
        internal RwMetarItem(int position, string value, string pattern) : base(position, value, pattern) { }
    }
}
