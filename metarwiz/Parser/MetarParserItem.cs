namespace ZippyNeuron.Metarwiz.Parser
{
    public class MetarParserItem
    {
        public MetarParserItem(int position, string item, MetarParserItemType type)
        {
            Position = position;
            Value = item;
            Type = type;
        }

        public int Position { get; }

        public string Value { get; }

        public MetarParserItemType Type { get; }
    }
}
