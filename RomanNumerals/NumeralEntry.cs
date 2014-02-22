using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class NumeralEntry
    {
        public NumeralEntry(string romanNumeral, int upperBound)
        {
            RomanNumeral = romanNumeral;
            UpperBound = upperBound;
        }

        public string RomanNumeral { get; set; }
        public int UpperBound { get; set; }

    }
}
