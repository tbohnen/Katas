using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class IntegerToRomanNumeralConverter
    {
        private readonly List<NumeralEntry> _numerals = new NumeralList();
        private string _convertedResult = "";
        private int _remainingIntegerToConvert;

        public string Convert(int integerToConvert)
        {
            _remainingIntegerToConvert = integerToConvert;

            EnumerateThroughNumeralsToConvertAllIntegers();
                
            return _convertedResult;
        }

        private void EnumerateThroughNumeralsToConvertAllIntegers()
        {
            foreach (var currentRomanNumeral in _numerals)
            {
                UseRomanNumeralToConvertRemaindingInteger(currentRomanNumeral);
                
                CalculateRemainderOfIntegerToConvertAfterCurrentConversion(currentRomanNumeral);

                if (_remainingIntegerToConvert == 0)
                    break;

            }
        }

        private void UseRomanNumeralToConvertRemaindingInteger(NumeralEntry romanNumeralToConvert)
        {
            int integerToConvertToNumeral = _remainingIntegerToConvert / romanNumeralToConvert.UpperBound;

            if (integerToConvertToNumeral > 0)
            {
                for (int i = 1; i <= integerToConvertToNumeral; i++)
                {
                    _convertedResult = string.Concat(_convertedResult, romanNumeralToConvert.RomanNumeral);
                }
            }
        }

        private void CalculateRemainderOfIntegerToConvertAfterCurrentConversion(NumeralEntry currentRomanNumeral)
        {
            _remainingIntegerToConvert = _remainingIntegerToConvert % currentRomanNumeral.UpperBound;
        }

    }
}
