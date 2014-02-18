using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class NumeralEntry
    {
        public NumeralEntry(string _romanNumeral, int _upperBound)
        {
            RomanNumeral = _romanNumeral;
            UpperBound = _upperBound;
            Completed = false;
        }

        public string RomanNumeral { get; set; }
        public int UpperBound { get; set; }
        public bool Completed { get; set; }

    }
}
