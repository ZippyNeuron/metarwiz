using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Abstractions
{
    public abstract class MvMetarItem : IMetarItem
    {
        internal MvMetarItem(int position, string value, string pattern)
        {
            Position = position;
            Value = value;
            Groups = Regex.Matches(value, pattern)[0].Groups;
        }

        protected GroupCollection Groups { get; }

        public int Position { get; set; }
        
        public string Value { get; set; }

        protected static bool Match(string value, string pattern) => Regex.IsMatch(value, pattern);

        public abstract override string ToString();
    }
}
