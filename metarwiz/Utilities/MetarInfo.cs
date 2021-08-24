using System;

namespace ZippyNeuron.Metarwiz.Utilities
{
    public class MetarInfo
    {
        private const string _remarksTag = "RMK";
        private const string _terminator = "=";
        private readonly string _metar;

        public MetarInfo(string metar)
        {
            _metar = metar;

            HasTerminator = _metar.EndsWith(_terminator);
            Metar = RemoveRemarks(RemoveTerminator(_metar));
            Remarks = GetRemarks(_metar);
        }

        public string Metar { get; }

        public string Remarks { get; }

        public string Terminator => (HasTerminator) ? _terminator : String.Empty;

        public bool HasTerminator { get; }

        public bool HasRemarks => !String.IsNullOrEmpty(Remarks);

        public string ToString(bool remarks = false)
        {
            return $"{Metar}{((remarks && HasRemarks) ? String.Concat(" ", Remarks) : String.Empty)}{Terminator}";
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
