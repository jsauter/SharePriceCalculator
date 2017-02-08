using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePriceCalculator.Core.Models;
using SharePriceCalculator.Services;

namespace SharePriceCalculator.Tests.ServiceTests
{
    [TestClass]
    public class TestOutputRenderer
    {
        [TestMethod]
        public void TestOutputGeneration()
        {
            var marketPrice = new MarketPrice();
            marketPrice.Price = 6.00M;
            marketPrice.MarketPriceDate = DateTime.Now;

            var anEmployee = new EmployeeShare();
            anEmployee.EmployeeId = "1";
            anEmployee.GrantPrice = 5.00M;
            anEmployee.NumberOfUnits = 100;
            anEmployee.VestDate = DateTime.Now.AddDays(-10);

            var anEmployee1 = new EmployeeShare();
            anEmployee1.EmployeeId = "1";
            anEmployee1.GrantPrice = 5.00M;
            anEmployee1.NumberOfUnits = 100;
            anEmployee1.VestDate = DateTime.Now.AddDays(-10);

            var anEmployee2 = new EmployeeShare();
            anEmployee2.EmployeeId = "2";
            anEmployee2.GrantPrice = 5.00M;
            anEmployee2.NumberOfUnits = 100;
            anEmployee2.VestDate = DateTime.Now.AddDays(-10);

            var employeeList = new List<EmployeeShare>();

            employeeList.Add(anEmployee);
            employeeList.Add(anEmployee1);
            employeeList.Add(anEmployee2);

            var outputRenderer = new OutputRendererService();

            var bonuses = new List<EmployeeBonus>();

            var result = outputRenderer.GenerateOutput(marketPrice, employeeList, bonuses, new List<EmployeeSale>());

            var resultCount = result.Split('\n').Length;

            Assert.IsTrue(resultCount == 3); // there is an extra line at the end, this is fine... 3 == 2, etc.
        }
    }
}
