using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class Translator
    {

        private readonly Dictionary<string, int> _numeralDivisionList = new Dictionary<string, int>();
        private int _numberToConvert = -1;

        public Translator()
        {
            SetupNumeralDivisionQueue();
        }

        private void SetupNumeralDivisionQueue()
        {
            _numeralDivisionList.Add("M", 1000);
            _numeralDivisionList.Add("D", 500);
            _numeralDivisionList.Add("C", 100);
            _numeralDivisionList.Add("L", 50);
            _numeralDivisionList.Add("X", 10);
            _numeralDivisionList.Add("I", 1);
        }


        public string Convert(int numberToConvert)
        {
            _numberToConvert = numberToConvert;

            var convertedResult = "";

            while (_numeralDivisionList.Count > 0 && _numberToConvert != 0)
            {
                convertedResult = string.Concat(convertedResult, GetNextRomanNumeral());
            }

            return convertedResult;
        }

        private string GetNextRomanNumeral()
        {
            var nextDivision = _numeralDivisionList.OrderByDescending(n => n.Value).First();
            int result = _numberToConvert / nextDivision.Value;

            var convertedResult = GetRomanNumber(result, nextDivision.Key);

            _numberToConvert = _numberToConvert % nextDivision.Value;
            _numeralDivisionList.Remove(nextDivision.Key);

            return convertedResult;
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
