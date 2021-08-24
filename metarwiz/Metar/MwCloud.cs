﻿using System;
using ZippyNeuron.Metarwiz.Abstractions;
using ZippyNeuron.Metarwiz.Enums;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwCloud : MvMetarItem
    {
        private const int _multiplier = 100;
        private readonly string _cloud;
        private readonly string _descriptor;
        private readonly int _altitude;
        private readonly CloudType _cloudType;

        public MwCloud(int position, string value) : base(position, value, Pattern)
        {
            _ = int.TryParse(Groups["ALTITUDE"].Value, out _altitude);
            _ = Enum.TryParse<CloudType>(Groups["CLOUD"].Value, out _cloudType);
            _cloud = Groups["CLOUD"].Value;
            _descriptor = Groups["DESCRIPTOR"].Value;
        }

        public int AboveGroundLevel => _altitude * _multiplier;

        public CloudType Cloud => _cloudType;

        private static string GetPattern()
        {
            string clouds = String
                .Join("|", Enum.GetNames<CloudType>());

            //return $@"^(?<CLOUD>{clouds})(?<ALTITUDE>\d*)(?<DESCRIPTOR>[A-Z]+|\/\/\/|)?$";
            return $@"^(?<CLOUD>\/\/\/|{clouds})(?<ALTITUDE>\d*|\/\/\/)(?<DESCRIPTOR>[A-Z]+|\/\/\/|)?$";
        }

        public static string Pattern => GetPattern();

        public static bool IsMatch(int position, string value) => Match(value, Pattern);

        public override string ToString()
        {
            string alt = (Cloud == CloudType.Unspecified) ? "///" : String.Format("{0:000}", _altitude);

            return String.Concat(
                (Cloud != CloudType.Unspecified) ? Enum.GetName<CloudType>(Cloud) : @"///",
                (Cloud != CloudType.NCD) ? alt : String.Empty,
                _descriptor
            ); ;
        }
    }
}