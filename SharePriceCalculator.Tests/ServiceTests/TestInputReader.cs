using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePriceCalculator.Core.Exceptions;
using SharePriceCalculator.Services;

namespace SharePriceCalculator.Tests.ServiceTests
{
    [TestClass]
    public class TestInputReader
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void TestThrowsInputInvalidException()
        {
            var recordFactory = new RecordFactory();

            var inputService = new InputReaderService(recordFactory);

            inputService.ReadInput("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void TestFirstLineIsNotIntegerThrowsInvalidException()
        {
            var recordFactory = new RecordFactory();

            var inputService = new InputReaderService(recordFactory);

            inputService.ReadInput("bob");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void TestMismatchedRowNumbers()
        {
            var recordFactory = new RecordFactory();

            var input = "5" + Environment.NewLine +
                        "VEST,001B,20120101,1000,0.45" + Environment.NewLine +
                        "VEST,002B,20130101,1000,0.50" + Environment.NewLine +
                        "VEST,001B,20130101,1500,0.50" + Environment.NewLine +
                        "VEST,003B,20130101,1000,0.50" + Environment.NewLine +
                        "20140101,1.00";

            var inputService = new InputReaderService(recordFactory);

            var result = inputService.ReadInput(input);            
        }

        [TestMethod]
        public void TestValidInputStream()
        {
            var recordFactory = new RecordFactory();

            var input = "5" + Environment.NewLine +
                        "VEST,001B,20120101,1000,0.45" + Environment.NewLine +
                        "VEST,002B,20120101,1500,0.45" + Environment.NewLine +
                        "VEST,002B,20130101,1000,0.50" + Environment.NewLine +
                        "VEST,001B,20130101,1500,0.50" + Environment.NewLine +
                        "VEST,003B,20130101,1000,0.50" + Environment.NewLine +
                        "20140101,1.00";

            var inputService = new InputReaderService(recordFactory);

            var result = inputService.ReadInput(input);

            Assert.IsTrue(result.ShareRecords.Count() == 5);            
            Assert.IsTrue(result.MarketPrice.Price == 1.00M);
            Assert.IsTrue(result.MarketPrice.MarketPriceDate == new DateTime(2014,1,1));
        }
    }
}
