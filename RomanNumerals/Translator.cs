using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class IntegerToRomanNumeralConverter
    {
        private readonly List<NumeralEntry> _numerals = new NumeralList();
        private string _convertedNumeralResult = "";
        private int _remainingIntegerToConvert;

        public string Convert(int integerToConvert)
        {
            _remainingIntegerToConvert = integerToConvert;

            EnumerateThroughNumeralsToConvertAllIntegers();
                
            return _convertedNumeralResult;
        }

        private void EnumerateThroughNumeralsToConvertAllIntegers()
        {
            foreach (var currentNumeralEntry in _numerals)
            {
                UseRomanNumeralToConvertRemaindingInteger(currentNumeralEntry);
                
                CalculateRemainderOfIntegerToConvertAfterCurrentConversion(currentNumeralEntry);

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
                    _convertedNumeralResult = string.Concat(_convertedNumeralResult, romanNumeralToConvert.RomanNumeral);
                }
            }
        }

        private void CalculateRemainderOfIntegerToConvertAfterCurrentConversion(NumeralEntry currentNumeral)
        {
            _remainingIntegerToConvert = _remainingIntegerToConvert % currentNumeral.UpperBound;
        }

    }
}
