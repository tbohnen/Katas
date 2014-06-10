using System;
using System.Collections.Generic;
using System.Linq;
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

            var numbers = GetNumberArrayFromString(input, delimiters).ToList();

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

        private static IEnumerable<int> GetNumberArrayFromString(string input, List<char> delimiters)
        {
            return input
                .Split(delimiters.ToArray())
                .Select(c => string.IsNullOrEmpty(c) ? 0 : Convert.ToInt32(c));
        }

        private static bool HasDelimiterSpecifier(string input)
        {
            return input.StartsWith("//");
        }

        private static string RemoveDelimiterSpecifierFromInput(string input)
        {
            return input.Substring(input.IndexOf('\n') + 1);
        }

        private List<char> GetDelimiters(string input)
        {
            var delimiters = new List<Char> { ',', '\n' };

            if (!HasDelimiterSpecifier(input)) return delimiters;

            var delimiter = input.Split('\n')[0].Substring(2);

            delimiters.Add(Convert.ToChar(delimiter));

            return delimiters;
        }
    }
}