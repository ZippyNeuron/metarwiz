﻿using ZippyNeuron.Metarwiz.Abstractions;

namespace ZippyNeuron.Metarwiz.Metar
{
    public class MwLocation : MwMetarItem
    {
        private readonly string _icao;

        public MwLocation(int position, string value) : base(position, value, Pattern)
        {
            _icao = Groups["ICAO"].Value;
        }

        public string ICAO => _icao;

        public static string Pattern => @"^(?<ICAO>[A-Z]{4})$";

        public static bool IsMatch(int position, string value) => position == 2 && Match(value, Pattern);

        public override string ToString()
        {
            return _icao;
        }
    }
}
