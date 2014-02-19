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
            _numeralDivisionList.Add(new NumeralEntry("M", 1000,900,false));
            _numeralDivisionList.Add(new NumeralEntry("D", 500,400,true));
            _numeralDivisionList.Add(new NumeralEntry("C", 100,90,false));
            _numeralDivisionList.Add(new NumeralEntry("L", 50,40,true));
            _numeralDivisionList.Add(new NumeralEntry("X", 10,9,false));
            _numeralDivisionList.Add(new NumeralEntry("V", 5,4,true));
            _numeralDivisionList.Add(new NumeralEntry("I", 1,1,false));
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
            NumeralEntry currentNumeralEntry = CurrentNumeralEntry();

            int currentNumber = CurrentNumberToConvert();
            if (nextRomanNumeral)
                currentNumber = currentNumber % CurrentNumeralEntry().UpperBound;

            int numberToTranslate = currentNumber / currentNumeralEntry.HighBound;
            if (numberToTranslate > 0 && CurrentNumeralEntry().UpperBound > currentNumber)
            {
                int lowerBoundNumeralIndex = -1;
                if (!currentNumeralEntry.MiddleRangeNumeral)
                    lowerBoundNumeralIndex  = _numeralDivisionList.IndexOf(currentNumeralEntry) + 2;
                else
                    lowerBoundNumeralIndex = _numeralDivisionList.IndexOf(currentNumeralEntry) + 1;

                lowerBoundNumeralEntry = _numeralDivisionList[lowerBoundNumeralIndex];
                _convertedNumeralResult = string.Concat(_convertedNumeralResult, lowerBoundNumeralEntry.RomanNumeral, currentNumeralEntry.RomanNumeral);
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
