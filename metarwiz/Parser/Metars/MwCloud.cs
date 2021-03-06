using System;
using System.Text.RegularExpressions;
using ZippyNeuron.Metarwiz.Enums;
using ZippyNeuron.Metarwiz.Extensions;

namespace ZippyNeuron.Metarwiz.Parser.Metars
{
    public class MwCloud : BaseMetarItem
    {
        private const int _multiplier = 100;
        private readonly string _descriptor;
        private readonly int _altitude;
        private readonly CloudType _cloudType;

        public MwCloud(Match match)
        {
            _ = int.TryParse(match.Groups["ALTITUDE"].Value, out _altitude);
            _ = Enum.TryParse(match.Groups["CLOUD"].Value, out _cloudType);
            _descriptor = match.Groups["DESCRIPTOR"].Value;
        }

        public int AboveGroundLevel => _altitude * _multiplier;

        public CloudType Cloud => _cloudType;

        public string CloudDescription => Cloud.GetDescription();

        private static string GetPattern()
        {
            string clouds = String
                .Join("|", Enum.GetNames<CloudType>());

            return $@"\ (?<CLOUD>\/\/\/|{clouds})(?<ALTITUDE>\d*|\/\/\/)(?<DESCRIPTOR>[A-Z]+|\/\/\/|)?";
        }

        public static string Pattern => GetPattern();

        public override string ToString()
        {
            string alt = (Cloud == CloudType.Unspecified) ? "///" : String.Format("{0:000}", _altitude);

            return String.Concat(
                (Cloud != CloudType.Unspecified) ? Enum.GetName(Cloud) : @"///",
                (_altitude == 0) ? String.Empty : alt,
                _descriptor
            ); ;
        }
    }
}
