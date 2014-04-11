using System;
using NUnit.Framework;

namespace YieldReturnKata
{
    [TestFixture]
    public class YielderTests
    {

        [Test]
        public void TestThatEveryValueIsOneGreaterThanLastValue()
        {
            var yieldReturnRunner = new YieldReturnRunner();
            var oldValue = -1;

            foreach (var value in yieldReturnRunner.GetValue())
            {
               Assert.AreEqual(oldValue + 1, value);
                oldValue = value;
            }
        }

    }
}