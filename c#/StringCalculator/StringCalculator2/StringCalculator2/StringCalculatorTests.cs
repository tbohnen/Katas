using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculator2
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private readonly StringCalculator _calculator = new StringCalculator();

        [Test]
        public void AddEmptyStringReturnsZero()
        {
            int summedValue = _calculator.Add("");

            Assert.AreEqual(0, summedValue);
        }

        [Test]
        public void AddOneValueReturnsSameValue()
        {
            int summedValue = _calculator.Add("5");

            Assert.AreEqual(5, summedValue);
        }

        [Test]
        public void AddTwoValuesReturnsSum()
        {
            var summedValue = _calculator.Add("1,2");

            Assert.AreEqual(3, summedValue);
        }

        [Test]
        public void AddMoreThanTwoValuesReturnsSum()
        {
            var summedValue = _calculator.Add("1,2,3");

            Assert.AreEqual(6, summedValue);
        }

        [Test]
        public void UseNewLineAsDelimeterWithCommas()
        {
            var summedValue = _calculator.Add("1\n2,3");

            Assert.AreEqual(6, summedValue);
        }

        [Test]
        public void SpecifyDelimeterInBeginningOfString()
        {
            var summedValue = _calculator.Add("//;\n1;2,3");

            Assert.AreEqual(6, summedValue);
        }


        [Test]
        public void NegativeNumbersThrowsExceptionWithNegativeNumbersInString()
        {
            var message = Assert.Throws<Exception>(() => _calculator.Add("-1,2,-3"));

            Assert.AreEqual("-1,-3", message.Message);
        }

        [Test]
        public void NumbersBiggerThan1000ShouldBeIgnored()
        {
            var summedValue = _calculator.Add("1005,2,3");

            Assert.AreEqual(5, summedValue);
        }

        [Test]
        public void DelimeterOfAnyLengthSupported()
        {
            var summedValue = _calculator.Add("//[***]\n1***2***3,4");

            Assert.AreEqual(10, summedValue);
        }

        [Test]
        public void MultipleCustomDelimetersSupported()
        {
            var summedValue = _calculator.Add("//[***][;;;]\n1***2***3;;;4");

            Assert.AreEqual(10, summedValue);
        }


    }

    public class StringCalculator
    {
        public int Add(string inputValues)
        {
            if (string.IsNullOrEmpty(inputValues)) return 0;

            if (inputValues.StartsWith("//"))
            {
                inputValues = GetAndReplaceCustomDelimeterWithDefaultDelimeter(inputValues);
            }

            var numbersToAdd = NumbersToAddExcludingNumbersBiggerThanThousand(inputValues).ToList();

            EnsureAllNumbersNotNegative(numbersToAdd);

            return numbersToAdd.Sum(number => Convert.ToInt32(number));
        }

        private void EnsureAllNumbersNotNegative(IEnumerable<int> numbersToAdd)
        {
            var negativeNumbers = numbersToAdd.Where(number => number < 0).ToList();

            if (negativeNumbers.Count <= 0) return;

            var exceptionMessage = String.Join(",", negativeNumbers);
            throw new Exception(exceptionMessage);
        }


        private static IEnumerable<int> NumbersToAddExcludingNumbersBiggerThanThousand(string inputValues)
        {
            var numbersToAdd = inputValues
                .Replace("\n", ",")
                .Split(',')
                .Select(n => string.IsNullOrEmpty(n) ? 0 : Convert.ToInt32(n))
                .Where(n => n <= 1000);

            return numbersToAdd;
        }

        private static string GetAndReplaceCustomDelimeterWithDefaultDelimeter(string inputValues)
        {
            var delimiter = GetExtraDelimeters(inputValues);
            inputValues = RemoveCustomDelimeter(inputValues);
            inputValues = ReplaceCustomDelimetersWithDefaultDelimeter(inputValues, delimiter);
            return inputValues;
        }

        private static string ReplaceCustomDelimetersWithDefaultDelimeter(string inputValues, IEnumerable<string> delimiters)
        {
            foreach (var delimeter in delimiters.Where(d => !string.IsNullOrEmpty(d)))
            {
                inputValues = inputValues.Replace(delimeter, ",");
            }

            return inputValues;
        }

        private static string RemoveCustomDelimeter(string inputValues)
        {
            inputValues = inputValues.Substring(inputValues.IndexOf('\n') + 1);
            return inputValues;
        }

        private static IEnumerable<string> GetExtraDelimeters(string inputValues)
        {
            var delimeter = inputValues.Split('\n')[0].Substring(2).Replace("]", "");

            return delimeter.Split('[').Where(d => !string.IsNullOrEmpty(d));
        }
    }
}
