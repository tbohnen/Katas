using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculator12
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        public void EmptyStringReturnsZero()
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add("");

            Assert.AreEqual(0, sum);
        }
        [Test]
        public void OneValueAddedReturnsSameValue()
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add("10");

            Assert.AreEqual(10, sum);
        }

        [TestCase("10,11", 21)]
        [TestCase("1,2,3", 6)]
        [TestCase("10,11,1", 22)]
        public void TwoValuesAddedReturnsSum(string values, int result)
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add(values);

            Assert.AreEqual(result, sum);
        }
        [Test]
        public void HandleNewLineAsDelimeterInsteadOfComma()
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add("10\n2");

            Assert.AreEqual(12, sum);
        }
        [Test]
        public void SpecifyCustomDelimeter()
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add("//;\n1;2");

            Assert.AreEqual(3, sum);
        }
        [Test]
        public void AddingNegativeNumbersThrowsException()
        {
            var calculator = new StringCalculator();

            var exception = Assert.Throws<NegativesNotSupported>(() => calculator.Add("-1,-2"));

            Assert.AreEqual(exception.Message, "negatives not allowed: -1,-2");
        }

        [Test]
        public void NumbersBiggerThanThousandIgnored()
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add("1001,1,2");

            Assert.AreEqual(3, sum);
        }
        [Test]
        public void SupportDelimetersWithLengthGreaterThanOne()
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add("//[***]\n1***3");

            Assert.AreEqual(4, sum);
        }

        [Test]
        public void SupportMultipleCustomDelimeters()
        {
            var calculator = new StringCalculator();

            var sum = calculator.Add("//[***][&&&]\n1***3&&&4");

            Assert.AreEqual(8, sum);
        }
    }

    public class StringCalculator
    {
        private static List<string> _delimeters = new List<string> { Comma, NewLine };
        private const string Comma = ",";
        private const string NewLine = "\n";
        private const string CustomDelimeterIndicator = "//";
        private const string NegativeExceptionMessage = "negatives not allowed: {0}";
        private const char OpenBlockChar = '[';
        private const char CloseBlockChar = ']';
        private const string MultipleCustomDelimeterSplitIndicator = "][";

        public int Add(string inputValues)
        {
            AddCustomDelimetersIfExists(inputValues);

            var numbers = SplitIntoNumbers(inputValues);

            EnsureNoNegativeNumbers(numbers);

            numbers = RemoveNumbersGreaterThanThousand(numbers);

            return SumNumbers(numbers);
        }

        private static IEnumerable<int> RemoveNumbersGreaterThanThousand(IEnumerable<int> numbers)
        {
            numbers = numbers.Where(n => n <= 1000);
            return numbers;
        }

        private void EnsureNoNegativeNumbers(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(n => n < 0);

            if (negativeNumbers.Any())
            {
                var message = String.Format(NegativeExceptionMessage, String.Join(Comma, negativeNumbers));
                throw new NegativesNotSupported(message);
            }
        }

        private static int SumNumbers(IEnumerable<int> numbers)
        {
            return numbers.Sum(v => Convert.ToInt32(v));
        }

        private static IEnumerable<int> SplitIntoNumbers(string inputValues)
        {
            string numbers = GetNumbersWithoutDelimeterSpecifierIfExists(inputValues);

            return numbers.Split(_delimeters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(n => Convert.ToInt32(n));
        }

        private static string GetNumbersWithoutDelimeterSpecifierIfExists(string inputValues)
        {
            if (HasCustomDelimeter(inputValues)) return inputValues.Substring(inputValues.IndexOf(NewLine) + 1);

            return inputValues;
        }

        private static void AddCustomDelimetersIfExists(string inputValues)
        {
            if (!HasCustomDelimeter(inputValues)) return;

            var delimeter = GetDelimeterSpecifier(inputValues);
            var delimeters = SplitCustomDelimeters(delimeter);
            _delimeters.AddRange(delimeters);
        }

        private static string GetDelimeterSpecifier(string inputValues)
        {
            var delimeter = inputValues.Substring(0, inputValues.IndexOf(NewLine));
            delimeter = delimeter.Substring(CustomDelimeterIndicator.Length);
            delimeter = RemoveUnusedChars(delimeter);
            return delimeter;
        }

        private static string[] SplitCustomDelimeters(string delimeter)
        {
            var delimeters = delimeter.Split(new[] { MultipleCustomDelimeterSplitIndicator },
                StringSplitOptions.RemoveEmptyEntries);
            return delimeters;
        }

        private static string RemoveUnusedChars(string delimeter)
        {
            delimeter = delimeter.TrimStart(OpenBlockChar).TrimEnd(CloseBlockChar);
            return delimeter;
        }

        private static bool HasCustomDelimeter(string inputValues)
        {
            return inputValues.StartsWith(CustomDelimeterIndicator);
        }
    }

    internal class NegativesNotSupported : Exception
    {
        public NegativesNotSupported(string message)
            : base(message)
        {

        }
    }
}
