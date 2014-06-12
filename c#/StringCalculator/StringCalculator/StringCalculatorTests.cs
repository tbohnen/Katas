using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace StringCalculator
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private readonly StringCalculator _stringCalculator = new StringCalculator();

        [Test]
        public void EmptyInputReturnsZero()
        {
            var sum = _stringCalculator.Add("");

            Assert.AreEqual(0, sum);
        }

        [Test]
        public void OneNumberReturnsSameNumber()
        {
            var sum = _stringCalculator.Add("10");

            Assert.AreEqual(10, sum);
        }


        [Test]
        public void TwoNumbersReturnsSum()
        {
            var sum = _stringCalculator.Add("10,12");

            Assert.AreEqual(22, sum);
        }

        [Test]
        public void ThreeNumbersReturnsSum()
        {
            var sum = _stringCalculator.Add("1,2,3");

            Assert.AreEqual(6, sum);
        }

        [Test]
        public void NewLineCharacterAsDelimiterTogetherWithComma()
        {
            var sum = _stringCalculator.Add("1\n2,3");

            Assert.AreEqual(6, sum);
        }

        [Test]
        public void SetDelimiterAtBeginningOfLine()
        {
            var sum = _stringCalculator.Add("//;\n1;3;6");

            Assert.AreEqual(10, sum);
        }

        [Test]
        public void NegativeNumbersThrowsExceptionWithListOfNegativeNumbers()
        {
            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add("-1,-2,3"));

            Assert.AreEqual(exception.Message, "-1,-2");
        }

        [Test]
        public void NumbersBiggerThanThousandShouldBeIgnored()
        {
            var sum = _stringCalculator.Add("1,2,3,1001,1002,4");

            Assert.AreEqual(10, sum);
        }

        [Test]
        public void DelimeterSpecifiedCanBeMoreThanOneCharInLength()
        {
            var sum = _stringCalculator.Add("//[|||]\n1|||2|||3");

            Assert.AreEqual(6, sum);
        }

        [Test]
        public void AllowMultipleCustomSpecifiedDelimeters()
        {
            var sum = _stringCalculator.Add("//[*][%]\n1*2%3");

	    Assert.AreEqual(6, sum);

        }

        [Test]
        public void AllowMultipleCustomSpecifiedDelimetersWithMultipleChars()
        {
            var sum = _stringCalculator.Add("//[**][%]\n1**2%3");

            Assert.AreEqual(6, sum);

        }



    }

    public class StringCalculator
    {
        public int Add(string input)
        {
            var delimiters = GetDelimiters(input);

            if (HasDelimiterSpecifier(input))
            {
                input = RemoveDelimiterSpecifierFromInput(input);
            }

            var numbers = GetNumberArrayFromStringExcludingNumbersGreaterThan1000(input, delimiters)
                .ToList();

            ThrowExceptionIfThereAreAnyNegativeNumbers(numbers);

            return numbers.Sum(number => number);
        }

        private void ThrowExceptionIfThereAreAnyNegativeNumbers(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0).ToList();

            if (!negativeNumbers.Any()) return;

            var exceptionMessage = String.Join(",", negativeNumbers.Select(n => n.ToString()));

            throw new Exception(exceptionMessage);
        }

        private static IEnumerable<int> GetNumberArrayFromStringExcludingNumbersGreaterThan1000(string input, List<string> delimiters)
        {
            var numbers = SplitNumbers(input, delimiters)
                .Select(c => string.IsNullOrEmpty(c) ? 0 : Convert.ToInt32(c))
                .Where(n => n < 1001);
            return numbers;
        }

        private static IEnumerable<string> SplitNumbers(string input, IEnumerable<string> delimiters)
        {

            var workingString = input;
            var outputStrings = new List<string>();

            foreach (var delimiter in delimiters)
            {
                bool containedDelimiter = false;
                while (workingString.Contains(delimiter))
                {
                    containedDelimiter = true;
                    int startIndexOfDelimiter = workingString.IndexOf(delimiter);

                    var number = workingString.Substring(0, startIndexOfDelimiter);

                    outputStrings.Add(number);

                    workingString = workingString.Substring(startIndexOfDelimiter + delimiter.Length);
                }

                if (containedDelimiter)
                {
                    outputStrings.Add(workingString);
                    workingString = ""; 
                }
            }

            if (!string.IsNullOrEmpty(workingString))
                outputStrings.Add(workingString);

            foreach (var delimiter in delimiters)
            {
                var listToEnumerate = outputStrings.ToList();
                foreach (var outputString in listToEnumerate)
                {
                    bool containedDelimiter = false;

                    workingString = outputString;

                    while (workingString.Contains(delimiter))
                    {
                        containedDelimiter = true;

                        int startIndexOfDelimiter = workingString.IndexOf(delimiter);

                        var number = workingString.Substring(0, startIndexOfDelimiter);

                        outputStrings.Add(number);

                        workingString = workingString.Substring(startIndexOfDelimiter + delimiter.Length);
                    }

                    if (containedDelimiter)
                        outputStrings.Add(workingString);

                }

            }

            List<string> outputStringsToRemove = new List<string>();

            foreach (var delimiter in delimiters)
            {
                foreach (var outputString in outputStrings)
                {
                    if (outputString.Contains(delimiter))
                    {
                        outputStringsToRemove.Add(outputString);
                    }
                }
            }

            foreach (var outputStringToRemove in outputStringsToRemove)
            {
                outputStrings.Remove(outputStringToRemove);
            }


            return outputStrings.ToArray();
        }

        private static bool HasDelimiterSpecifier(string input)
        {
            return input.StartsWith("//");
        }

        private static string RemoveDelimiterSpecifierFromInput(string input)
        {
            return input.Substring(input.IndexOf('\n') + 1);
        }

        private List<string> GetDelimiters(string input)
        {
            var delimiters = new List<string> { ",", "\n" };

            if (!HasDelimiterSpecifier(input)) return delimiters;

            var delimiter = input.Split('\n')[0].Substring(2);

            var specialDelimitersInDelimiters = delimiter.Split('[');

            foreach (var specialDelimitersInDelimiter in specialDelimitersInDelimiters)
            {
                if (string.IsNullOrEmpty(specialDelimitersInDelimiter)) continue;

                delimiters.Add(specialDelimitersInDelimiter.Replace("]",""));
            }

            return delimiters;
        }
    }
}