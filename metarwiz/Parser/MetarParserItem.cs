namespace ZippyNeuron.Metarwiz.Parser
{
    public class MetarParserItem
    {
        public MetarParserItem() { }

        public int Index { get; set; }

        public BaseMetarItem Item { get; set; }

        public MetarParserItemType Type { get; set;  }
    }
}
