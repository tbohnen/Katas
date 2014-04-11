using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class IntegerToRomanNumeralConverter
    {
        private readonly List<RomanNumeral> _numerals = new NumeralList();
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
            _numerals.ForEach(currentRomanNumeral => 
            {
                UseRomanNumeralToConvertRemaindingInteger(currentRomanNumeral);

                CalculateRemainderOfIntegerToConvertAfterCurrentConversion(currentRomanNumeral);
            });
            
        }

        private void UseRomanNumeralToConvertRemaindingInteger(RomanNumeral romanNumeralToConvert)
        {
            int integerToConvertToNumeral = _remainingIntegerToConvert / romanNumeralToConvert.IntegerEquivalent;

            for (int i = 0; i < integerToConvertToNumeral; i++)
                _convertedResult = string.Concat(_convertedResult, romanNumeralToConvert.Numeral);
        }

        private void CalculateRemainderOfIntegerToConvertAfterCurrentConversion(RomanNumeral currentRomanNumeral)
        {
            _remainingIntegerToConvert = _remainingIntegerToConvert % currentRomanNumeral.IntegerEquivalent;
        }
    }
}
