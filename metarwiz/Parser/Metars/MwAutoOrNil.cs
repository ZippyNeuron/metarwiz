using System.Text.RegularExpressions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwAutoOrNil : BaseMetarItem
    {
        private readonly string _type;

        public MwAutoOrNil(Match match)
        {
            _type = match.Groups["TYPE"].Value;
        }

        public bool IsAuto => _type == "AUTO";
        public bool IsNil => _type == "NIL";

        public static string Pattern => @"( )(?<TYPE>AUTO|NIL)";

        public override string ToString()
        {
            return _type;
        }
    }
}
