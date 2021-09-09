using System;

namespace ZippyNeuron.Metarwiz.Parser
{
    public class MetarInfo
    {
        private const string _remarksTag = "RMK";
        private const string _terminator = "=";

        protected internal MetarInfo(string metar, string tag)
        {
            Original = metar;
            HasTerminator = metar.EndsWith(_terminator);
            Metar = RemoveRemarks(RemoveTerminator(metar));
            Remarks = GetRemarks(metar);
            Tag = tag;
        }

        public string Tag { get; }

        public string Original { get; }

        public string Metar { get; }

        public string Remarks { get; }

        public string Terminator => (HasTerminator) ? _terminator : String.Empty;

        public bool HasTerminator { get; }

        public bool HasRemarks => !String.IsNullOrEmpty(Remarks);

        public override string ToString()
        {
            return $"{Metar}{((HasRemarks) ? String.Concat(" ", Remarks) : String.Empty)}{Terminator}";
        }

        private string RemoveTerminator(string metar)
        {
            return metar
                .Trim()
                .TrimEnd(_terminator.ToCharArray())
                .Trim();
        }

        private string GetRemarks(string metar)
        {
            int start = metar.IndexOf(_remarksTag);

            if (start < 0)
                return String.Empty;

            return metar
                .Substring(start)
                .TrimEnd(_terminator.ToCharArray());
        }

        private string RemoveRemarks(string metar)
        {
            int start = metar.IndexOf(_remarksTag);

            if (start < 0)
                return metar;

            return metar.Substring(0, start - 1)
                .Trim();
        }
    }
}
