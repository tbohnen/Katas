using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class RomanNumeral
    {
        public RomanNumeral(string numeral, int integerEquivalent)
        {
            Numeral = numeral;
            IntegerEquivalent = integerEquivalent;
        }

        public string Numeral { get; set; }
        public int IntegerEquivalent { get; set; }

    }
}
