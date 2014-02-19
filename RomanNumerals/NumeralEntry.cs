using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class NumeralEntry
    {
        public NumeralEntry(string romanNumeral, int upperBound, int highBound, bool middleRangeNumeral)
        {
            RomanNumeral = romanNumeral;
            UpperBound = upperBound;
            Completed = false;
            HighBound = highBound;
            MiddleRangeNumeral = middleRangeNumeral;
        }

        public string RomanNumeral { get; set; }
        public int UpperBound { get; set; }
        public int HighBound { get; set; }
        public bool Completed { get; set; }
        public bool MiddleRangeNumeral { get; set; }

    }
}
