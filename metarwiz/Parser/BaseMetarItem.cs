namespace ZippyNeuron.Metarwiz.Parser
{
    public abstract class BaseMetarItem : IMetarItem
    {
        public int Position { get; set; }

        public string Value => ToString();

        public abstract override string ToString();
    }
}
