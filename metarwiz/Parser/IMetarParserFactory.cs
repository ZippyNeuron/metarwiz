namespace ZippyNeuron.Metarwiz.Parser
{
    public interface IMetarParserFactory
    {
        IMetarItem Create(MetarParserItem item);
    }
}