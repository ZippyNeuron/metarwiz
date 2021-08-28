namespace ZippyNeuron.Metarwiz.Abstractions
{
    public abstract class MwMetarItem : BaseMetarItem, IMetarItem
    {
        internal MwMetarItem(int position, string value, string pattern) : base(position, value, pattern) { }
    }
}
