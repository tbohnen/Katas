using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class TestAssemblyTests
    {
        [Test]
        [UseReporter(typeof(NUnitReporter))]
        public void TestTheTruth()
        {
            new Program().Run();

            string outputFilePath = String.Format("{0}\\output.txt", AppDomain.CurrentDomain.BaseDirectory);

            Approvals.VerifyFile(outputFilePath);
        }

        [Test]
        public void ConjuredItemsDegradeInQualityTwiceAsFast()
        {
            var program = new Program();
            program.Items = new List<Item>()
	    {
	        new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
	    };

            program.UpdateItems();

            Assert.AreEqual(4, program.Items.First().Quality);
        }
    }
}