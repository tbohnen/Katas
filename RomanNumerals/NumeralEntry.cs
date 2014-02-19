using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class NumeralEntry
    {
        public NumeralEntry(string romanNumeral, int upperBound, int highBound)
        {
            RomanNumeral = romanNumeral;
            UpperBound = upperBound;
            Completed = false;
            HighBound = highBound;
        }

        public string RomanNumeral { get; set; }
        public int UpperBound { get; set; }
        public int HighBound { get; set; }
        public bool Completed { get; set; }

    }
}
