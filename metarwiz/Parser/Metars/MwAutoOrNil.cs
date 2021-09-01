using System;
using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwAutoOrNil : BaseMetarItem
    {
        private readonly string _auto;
        private readonly string _nil;

        public MwAutoOrNil(Match match)
        {
            _auto = match.Groups["AUTO"].Value;
            _nil = match.Groups["NIL"].Value;
        }

        public bool IsAutomated => _auto == "AUTO";

        public bool IsNil => _nil == "NIL";

        public static string Pattern => @"\ (?<AUTO>AUTO)|(?<NIL>NIL)";

        public override string ToString()
        {
            return (String.IsNullOrEmpty(_auto)) ? _nil : _auto;
        }
    }
}
