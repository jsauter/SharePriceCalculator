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
                    var newShareRecord = new EmployeeShare();

                    newShareRecord.Action = csvReader.GetField<string>(0);
                    newShareRecord.EmployeeId = csvReader.GetField<string>(1);

                    newShareRecord.VestDate = DateTime.ParseExact(csvReader.GetField<string>(2), "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None);
                    newShareRecord.NumberOfUnits = csvReader.GetField<int>(3);
                    newShareRecord.GrantPrice = csvReader.GetField<decimal>(4);

                    return newShareRecord;         
                case "PERF":
                    var employeeBonus = new EmployeeBonus();

                    employeeBonus.Action = csvReader.GetField<string>(0);
                    employeeBonus.EmployeeId = csvReader.GetField<string>(1);
                    employeeBonus.BonusDate = DateTime.ParseExact(csvReader.GetField<string>(2), "yyyyMMdd",
                            CultureInfo.InvariantCulture, DateTimeStyles.None);
                    employeeBonus.Multiplier = csvReader.GetField<decimal>(3);

                    return employeeBonus;
                case "SALE":
                    var employeeSale = new EmployeeSale();

                    employeeSale.Action = csvReader.GetField<string>(0);
                    employeeSale.EmployeeId = csvReader.GetField<string>(1);
                    employeeSale.SaleDate = DateTime.ParseExact(csvReader.GetField<string>(2), "yyyyMMdd",
                            CultureInfo.InvariantCulture, DateTimeStyles.None);
                    employeeSale.Quantity = csvReader.GetField<int>(3);
                    employeeSale.MarketSalePrice = csvReader.GetField<decimal>(4);

                    return employeeSale;
                default:
                    throw new InvalidInputException("Row type not supported.");                    
            }
        }
    }
}