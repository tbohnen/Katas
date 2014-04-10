using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class NumeralList : List<RomanNumeral>
    {
        public NumeralList()
        {
            this.Add(new RomanNumeral("M", 1000));
            this.Add(new RomanNumeral("CM", 900));
            this.Add(new RomanNumeral("D", 500));
            this.Add(new RomanNumeral("CD", 400));
            this.Add(new RomanNumeral("C", 100));
            this.Add(new RomanNumeral("XC", 90));
            this.Add(new RomanNumeral("L", 50));
            this.Add(new RomanNumeral("XL", 40));
            this.Add(new RomanNumeral("X", 10));
            this.Add(new RomanNumeral("IX", 9));
            this.Add(new RomanNumeral("V", 5));
            this.Add(new RomanNumeral("IV", 4));
            this.Add(new RomanNumeral("I", 1));
        }
    }
}
