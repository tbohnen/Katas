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
            _numeralDivisionList.Add(new NumeralEntry("M", 1000,900));
            _numeralDivisionList.Add(new NumeralEntry("D", 500,500));
            _numeralDivisionList.Add(new NumeralEntry("C", 100,90));
            _numeralDivisionList.Add(new NumeralEntry("L", 50,50));
            _numeralDivisionList.Add(new NumeralEntry("X", 10,9));
            _numeralDivisionList.Add(new NumeralEntry("V", 5,5));
            _numeralDivisionList.Add(new NumeralEntry("I", 1,1));
        }

        public string Convert(int numberToConvert)
        {
            _numberToConvert = numberToConvert;

            while (_numeralDivisionList.Count > 0 && CurrentNumberToConvert() != 0)
            {
                NumeralEntry lowerBoundNumeralEntry = null;

                var currentNumeralEntryUsed = UseNextRomanNumberAndReturnIfNumeralUsed();
                lowerBoundNumeralEntry = UseUpperBoundIfApplicableOrReturnNull(currentNumeralEntryUsed);

                if (lowerBoundNumeralEntry != null)
                {
                    _previouslyUsedNumeralEntry = lowerBoundNumeralEntry;
                    lowerBoundNumeralEntry.Completed = true;
                }
                else if (currentNumeralEntryUsed)
                {
                    _previouslyUsedNumeralEntry = CurrentNumeralEntry();
                }

                CurrentNumeralEntry().Completed = true;
            }

            return _convertedNumeralResult;
        }

        private NumeralEntry UseUpperBoundIfApplicableOrReturnNull(bool nextRomanNumeral)
        {
            NumeralEntry lowerBoundNumeralEntry = null;
            int currentNumber = CurrentNumberToConvert();
            if (nextRomanNumeral)
                currentNumber = currentNumber % CurrentNumeralEntry().UpperBound;

            int numberToTranslate = currentNumber / (CurrentNumeralEntry().HighBound);
            if (numberToTranslate > 0 && CurrentNumeralEntry().UpperBound > currentNumber)
            {
                int lowerBoundNumeralIndex = _numeralDivisionList.IndexOf(CurrentNumeralEntry()) + 2;
                lowerBoundNumeralEntry = _numeralDivisionList[lowerBoundNumeralIndex];
                _convertedNumeralResult = string.Concat(_convertedNumeralResult, lowerBoundNumeralEntry.RomanNumeral, CurrentNumeralEntry().RomanNumeral);
            }

            return lowerBoundNumeralEntry;
        }

        private bool UseNextRomanNumberAndReturnIfNumeralUsed()
        {
            bool worked = false;
            var currentNumeralEntry = CurrentNumeralEntry();
            int numberToTranslate = CurrentNumberToConvert() / currentNumeralEntry.UpperBound;

            if (numberToTranslate > 0)
            {
                for (int i = 1; i <= numberToTranslate; i++)
                {
                    _convertedNumeralResult = string.Concat(_convertedNumeralResult, currentNumeralEntry.RomanNumeral);
                    worked = true;
                }
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
