using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculator9
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private readonly StringCalculator _calculator = new StringCalculator();

        [Test]
        public void EmptyStringReturnsZero()
        {
            var sum = _calculator.Add("");

            Assert.AreEqual(0, sum);
        }

        [Test]
        public void OneValueAddedReturnsSameValue()
        {

            var sum = _calculator.Add("1");

            Assert.AreEqual(1, sum);
        }
        [Test]
        public void TwoValuesAddedReturnsSumOfValues()
        {
            var sum = _calculator.Add("1,2");

            Assert.AreEqual(3, sum);
        }
        [Test]
        public void ThreeValuesAddedReturnsSumOfValues()
        {
            var sum = _calculator.Add("1,2,3");

            Assert.AreEqual(6, sum);
        }
        [Test]
        public void NewLineSeperatorInsteadOfComma()
        {
            var sum = _calculator.Add("1\n2");

            Assert.AreEqual(3, sum);
        }
        [Test]
        public void SupportCustomDelimeter()
        {
            var sum = _calculator.Add("//;\n1;2");

            Assert.AreEqual(3, sum);
        }
        [Test]
        public void ThrowExceptionIfAnyNegativeNumbers()
        {
            var exception = Assert.Throws<Exception>(() => _calculator.Add("-1,-2"));

            Assert.AreEqual(exception.Message, "negatives not allowed: -1,-2");
        }
        [Test]
        public void IgnoreNumbersBiggerThanThousand()
        {
            var sum = _calculator.Add("1001,1,2");

            Assert.AreEqual(3, sum);
        }
        [Test]
        public void SupportDelimetersWithLengthMoreThanOne()
        {
            var sum = _calculator.Add("//[***]\n1***2");

            Assert.AreEqual(3, sum);
        }
        [Test]
        public void SupportMultipleCustomDelimeters()
        {
            var sum = _calculator.Add("//[***][&&&]\n1***2&&&1");

            Assert.AreEqual(4, sum);
        }
    }

    public class StringCalculator
    {
        private const string NegativesNotAllowedExceptionMessage = "negatives not allowed: {0}";
        private const string Comma = ",";
        private const string NewLine = "\n";
        private const char ForwardSlash = '/';
        private const char OpenBlock = '[';
        private const char CloseBlock = ']';
        private const string OpenCloseBlock = "][";

        public int Add(string inputValues)
        {
            if (string.Empty == inputValues) return 0;

            var numbers = SplitValuesIntoNumbers(inputValues).ToList();

            ValidateNoNegativeNumbers(numbers);

            numbers = RemoveNumbersBiggerThanThousand(numbers);

            return SumNumbers(numbers);
        }

        private List<int> RemoveNumbersBiggerThanThousand(IEnumerable<int> numbers)
        {
            return numbers.Where(n => n <= 1000).ToList();
        }

        private void ValidateNoNegativeNumbers(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(n => n < 0);

            if (negativeNumbers.Any())
            {
                ThrowNegativesNotAllowedException(negativeNumbers);
            }
        }

        private void ThrowNegativesNotAllowedException(IEnumerable<int> negativeNumbers)
        {
            var mesage = String.Format(NegativesNotAllowedExceptionMessage, String.Join(Comma, negativeNumbers));
            throw new Exception(mesage);
        }

        private static int SumNumbers(IEnumerable<int> numbers)
        {
            return numbers.Sum();
        }

        private static IEnumerable<int> SplitValuesIntoNumbers(string inputValues)
        {
            var delimeters = new List<string> { Comma , NewLine};

            if (HasCustomDelimeter(inputValues))
            {
                var delimeterString = GetCustomDelimeters(inputValues);

                delimeters.AddRange(delimeterString);

                inputValues = RemoveDelimeterString(inputValues);
            }

            var numbers = SplitNumbers(inputValues, delimeters);
            return numbers.Select(n => Convert.ToInt32(n));
        }

        private static string[] SplitNumbers(string inputValues, List<string> delimeters)
        {
            return inputValues.Split(delimeters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private static string RemoveDelimeterString(string inputValues)
        {
            return inputValues.Substring(inputValues.IndexOf(NewLine) + 1);
        }

        private static string[] GetCustomDelimeters(string inputValues)
        {
            var delimeterString = GetDelimeterSpecifier(inputValues);
            delimeterString = RemoveCharsOnlyUsedToIdentifyDelimeters(delimeterString);
            return SplitDelimeters(delimeterString);
        }

        private static string[] SplitDelimeters(string delimeterString)
        {
            return delimeterString.Split(new[] {OpenCloseBlock}, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetDelimeterSpecifier(string inputValues)
        {
            return inputValues.Substring(0, inputValues.IndexOf(NewLine));
        }

        private static string RemoveCharsOnlyUsedToIdentifyDelimeters(string delimeterString)
        {
            return delimeterString
                .TrimStart(ForwardSlash)
                .TrimStart(OpenBlock)
                .TrimEnd(Convert.ToChar(NewLine))
                .TrimEnd(CloseBlock);
        }

        private static bool HasCustomDelimeter(string inputValues)
        {
            return inputValues.StartsWith("//");
        }
    }
}
