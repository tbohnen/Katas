using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    [TestFixture]
    class TranslatorTests
    {

        [TestCase(1, "I")]
        [TestCase(2, "II")]
        [TestCase(9, "IX")]
        [TestCase(19, "XIX")]
        [TestCase(2000, "MM")]
        [TestCase(2500, "MMD")]
        [TestCase(2600, "MMDC")]
        [TestCase(2750, "MMDCCL")]
        [TestCase(2950, "MMCML")]
        [TestCase(2953, "MMCMLIII")]
        [TestCase(953, "CMLIII")]
        [TestCase(954, "CMLIV")]
        [TestCase(2445, "MMCDXLV")]
        [TestCase(2444, "MMCDXLIV")]
        [TestCase(44, "XLIV")]
        [TestCase(28, "XXVIII")]
        public void Translator_InstantiateTranslator(int numberToConvert, string expected)
        {
            var translator = new IntegerToRomanNumeralConverter();

            var convertedNumeral = translator.Convert(numberToConvert);

            Assert.AreEqual(expected, convertedNumeral);
        }

    }


}
