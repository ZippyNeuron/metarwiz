using System;
using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwAutoOrNil : MwMetarItem
    {
        private readonly string _auto;
        private readonly string _nil;

        public MwAutoOrNil(int position, string value) : base(position, value, Pattern)
        {
            _auto = Groups["AUTO"].Value;
            _nil = Groups["NIL"].Value;
        }

        public bool IsAutomated => _auto == "AUTO";

        public bool IsNil => _nil == "NIL";

        public static string Pattern => @"^(?<AUTO>AUTO)|(?<NIL>NIL)$";

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            return (String.IsNullOrEmpty(_auto)) ? _nil : _auto;
        }
    }
}
