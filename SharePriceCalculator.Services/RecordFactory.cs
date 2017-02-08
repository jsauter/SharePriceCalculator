using System;
using System.Globalization;
using CsvHelper;
using SharePriceCalculator.Core.Exceptions;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Services
{
    public class RecordFactory : IRecordFactory
    {
        public BaseRecord GetRecord(string type, CsvReader csvReader)
        {
            switch (type)
            {
                case "VEST":
                    var newShareRecord = new EmployeeShare
                    {
                        Action = csvReader.GetField<string>(0),
                        EmployeeId = csvReader.GetField<string>(1),
                        VestDate = ParseExactDate(csvReader.GetField<string>(2)),
                        NumberOfUnits = csvReader.GetField<int>(3),
                        GrantPrice = csvReader.GetField<decimal>(4)
                    };
                    return newShareRecord;         
                case "PERF":
                    var employeeBonus = new EmployeeBonus
                    {
                        Action = csvReader.GetField<string>(0),
                        EmployeeId = csvReader.GetField<string>(1),
                        BonusDate = ParseExactDate(csvReader.GetField<string>(2)),
                        Multiplier = csvReader.GetField<decimal>(3)
                    };
                    return employeeBonus;
                case "SALE":
                    var employeeSale = new EmployeeSale
                    {
                        Action = csvReader.GetField<string>(0),
                        EmployeeId = csvReader.GetField<string>(1),
                        SaleDate = ParseExactDate(csvReader.GetField<string>(2)),
                        Quantity = csvReader.GetField<int>(3),
                        MarketSellPrice = csvReader.GetField<decimal>(4)
                    };
                    return employeeSale;
                default:
                    throw new InvalidInputException("Row type not supported.");                    
            }
        }

        private static DateTime ParseExactDate(string dateString)
        {
            return DateTime.ParseExact(dateString, "yyyyMMdd",
                CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}