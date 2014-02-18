using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    class Translator
    {

        private readonly List<NumeralEntry> _numeralDivisionList = new List<NumeralEntry>();
        private int _numberToConvert;
        private NumeralEntry _previouslyUsedNumeralEntry;
        private string _convertedNumeralResult = "";

        public Translator()
        {
            SetupNumeralDivisionList();
        }

        private void SetupNumeralDivisionList()
        {
            _numeralDivisionList.Add(new NumeralEntry("M", 1000));
            _numeralDivisionList.Add(new NumeralEntry("D", 500));
            _numeralDivisionList.Add(new NumeralEntry("C", 100));
            _numeralDivisionList.Add(new NumeralEntry("L", 50));
            _numeralDivisionList.Add(new NumeralEntry("X", 10));
            _numeralDivisionList.Add(new NumeralEntry("V", 5));
            _numeralDivisionList.Add(new NumeralEntry("I", 1));
        }

        public string Convert(int numberToConvert)
        {
            _numberToConvert = numberToConvert;

            while (_numeralDivisionList.Count > 0 && CurrentNumberToConvert() != 0)
            {
                var nextRomanNumeral = UseNextRomanNumberAndReturnIfNumeralUsed();
                if (nextRomanNumeral)
                {
                    _previouslyUsedNumeralEntry = CurrentNumeralEntry();
                }
                CurrentNumeralEntry().Completed = true;
            }

            return _convertedNumeralResult;
        }

        private bool UseNextRomanNumberAndReturnIfNumeralUsed()
        {
            bool worked = false;

            int numberToTranslate = CurrentNumberToConvert() / CurrentNumeralEntry().UpperBound;

            for (int i = 1; i <= numberToTranslate; i++)
            {
                _convertedNumeralResult = string.Concat(_convertedNumeralResult, CurrentNumeralEntry().RomanNumeral);
                worked = true;
            }

            return worked;
        }

        private NumeralEntry CurrentNumeralEntry()
        {
            return _numeralDivisionList
                            .Where(n => !n.Completed)
                            .OrderByDescending(n => n.UpperBound).FirstOrDefault();
        }

        private int CurrentNumberToConvert()
        {
            var previousNumeralEntryIndex = _numeralDivisionList.IndexOf(_previouslyUsedNumeralEntry);

            if (previousNumeralEntryIndex < 0)
            {
                return _numberToConvert;
            }

            var previousNumeralEntry = _numeralDivisionList[previousNumeralEntryIndex];

            return _numberToConvert % previousNumeralEntry.UpperBound;
        }
    }
}
