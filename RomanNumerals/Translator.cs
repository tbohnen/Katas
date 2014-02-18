using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class Translator
    {
        public string Convert(int numberToConvert)
        {
            switch (numberToConvert)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                default:
                    {
                        var convertedResult = "";

                        int thousandResult = numberToConvert / 1000;
                        convertedResult = GetRomanNumber(thousandResult, "M");

                        numberToConvert = numberToConvert % 1000;
                        int hundredResult = numberToConvert / 500;
                        convertedResult = convertedResult + GetRomanNumber(hundredResult, "D");

                        numberToConvert = numberToConvert % 500;
                        hundredResult = numberToConvert / 100;
                        convertedResult = convertedResult + GetRomanNumber(hundredResult, "C");

                        return convertedResult;
                    }
            }
        }

        private static string GetRomanNumber(int thousandResult, string numeral)
        {
            string convertedResult = "";

            for (int i = 1; i <= thousandResult; i++)
            {
                convertedResult = string.Concat(convertedResult, numeral);
            }
            return convertedResult;
        }
    }
}
