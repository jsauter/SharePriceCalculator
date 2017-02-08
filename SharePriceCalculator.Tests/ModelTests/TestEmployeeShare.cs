using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Tests.ModelTests
{
    [TestClass]
    public class TestEmployeeShare
    {
        [TestMethod]
        public void TestVestDateAfterMarketDate()
        {
            var marketPrice = new MarketPrice();

            marketPrice.Price = 6.00M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var employeeShare = new EmployeeShare();

            employeeShare.GrantPrice = 5.00M;
            employeeShare.VestDate = DateTime.Now.AddDays(1);
            employeeShare.NumberOfUnits = 1;

            Assert.IsTrue(employeeShare.CalculateGains(marketPrice) == 0.00M);
        }

        [TestMethod]
        public void TestEmployeeShareLoss()
        {
            var marketPrice = new MarketPrice();

            marketPrice.Price = 6.00M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var employeeShare = new EmployeeShare();

            employeeShare.GrantPrice = 7.00M;
            employeeShare.VestDate = DateTime.Now.AddDays(1);
            employeeShare.NumberOfUnits = 1;

            Assert.IsTrue(employeeShare.CalculateGains(marketPrice) == 0.00M);
        }

        [TestMethod]
        public void TestEmployeeShareGain1Unit()
        {
            var marketPrice = new MarketPrice();

            marketPrice.Price = 6.00M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var employeeShare = new EmployeeShare();

            employeeShare.GrantPrice = 5.00M;
            employeeShare.VestDate = DateTime.Now.AddDays(-1);
            employeeShare.NumberOfUnits = 1;

            Assert.IsTrue(employeeShare.CalculateGains(marketPrice) == 1.00M);
        }

        [TestMethod]
        public void TestEmployeeShareGainRoundsDown()
        {
            var marketPrice = new MarketPrice();

            marketPrice.Price = 6.001M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var employeeShare = new EmployeeShare();

            employeeShare.GrantPrice = 5.00M;
            employeeShare.VestDate = DateTime.Now.AddDays(-1);
            employeeShare.NumberOfUnits = 1;

            Assert.IsTrue(employeeShare.CalculateGains(marketPrice) == 1.00M);
        }

        [TestMethod]
        public void TestEmployeeShareGainRoundsUp()
        {
            var marketPrice = new MarketPrice();

            marketPrice.Price = 6.005M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var employeeShare = new EmployeeShare();

            employeeShare.GrantPrice = 5.00M;
            employeeShare.VestDate = DateTime.Now.AddDays(-1);
            employeeShare.NumberOfUnits = 1;

            Assert.IsTrue(employeeShare.CalculateGains(marketPrice) == 1.01M);
        }

        [TestMethod]
        public void TestEmployeeShareGainLargeNumberOfShares()
        {
            var marketPrice = new MarketPrice();

            marketPrice.Price = 6.00M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var employeeShare = new EmployeeShare();

            employeeShare.GrantPrice = 5.00M;
            employeeShare.VestDate = DateTime.Now.AddDays(-1);
            employeeShare.NumberOfUnits = 1000;

            Assert.IsTrue(employeeShare.CalculateGains(marketPrice) == 1000.00M);
        }

        [TestMethod]
        public void TestEmployeeShareWithMultiplier()
        {
            var marketPrice = new MarketPrice();

            marketPrice.Price = 6.00M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var employeeShare = new EmployeeShare();
            var employeeBonus = new EmployeeBonus();

            employeeBonus.BonusDate = DateTime.Now;
            employeeBonus.Multiplier = 2.00M;
            employeeBonus.EmployeeId = "123";

            employeeShare.EmployeeId = "123";
            employeeShare.GrantPrice = 5.00M;
            employeeShare.VestDate = DateTime.Now.AddDays(-1);
            employeeShare.NumberOfUnits = 1000;

            Assert.IsTrue(employeeShare.CalculateGains(marketPrice, employeeBonus) == 2000.00M);
        }
    }
}
