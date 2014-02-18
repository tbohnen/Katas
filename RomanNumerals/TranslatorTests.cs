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
        [TestCase(2000, "MM")]
        [TestCase(2500, "MMD")]
        [TestCase(2600, "MMDC")]
        public void Translator_InstantiateTranslator(int numberToConvert, string expected)
        {
            var translator = new Translator();

            var convertedNumeral = translator.Convert(numberToConvert);

            Assert.AreEqual(expected, convertedNumeral);
        }

    }


}
