using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Tests.ModelTests
{
    [TestClass]
    public class TestShareSales
    {
        [TestMethod]
        public void TestSellOfShare()
        {

            var shares = new List<EmployeeShare>();

            shares.Add(new EmployeeShare()
            {
                GrantPrice = .45M,
                NumberOfUnits = 1000,
                VestDate = DateTime.Now.AddDays(-10)
            });

            var bonus = new EmployeeBonus();
            bonus.BonusDate = DateTime.Now.AddDays(10);
            bonus.Multiplier = 1.5M;

            var shareSell = new EmployeeSale();

            shareSell.Quantity = 500;
            shareSell.SaleDate = DateTime.Now;
            shareSell.MarketSellPrice = 1.00M;

            var bonuslist = new List<EmployeeBonus>();
            
            bonuslist.Add(bonus);

            var result = shareSell.CalculateSale(shares, bonuslist);

            Assert.IsTrue(result == 275);            
        }
    }
}
