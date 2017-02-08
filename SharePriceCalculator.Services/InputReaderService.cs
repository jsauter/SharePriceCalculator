using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using CsvHelper.TypeConversion;
using SharePriceCalculator.Core.Exceptions;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Services
{
    public class InputReaderService : IInputReaderService
    {
        private readonly IRecordFactory _recordFactory;

        public InputReaderService(IRecordFactory recordFactory)
        {
            _recordFactory = recordFactory;
        }

        public ShareRun ReadInput(string inputValue)
        {
            var shareRun = new ShareRun();

            int rowCount = 0;
            int numberOfDataRows = 0;            

            if (string.IsNullOrEmpty(inputValue))
            {
                throw new InvalidInputException("Input string is empty or null.");
            }

            rowCount = inputValue.Split('\n').Length;

            var stringReader = new StringReader(inputValue);            
            var csvReader = new CsvHelper.CsvReader(stringReader);
            csvReader.Configuration.HasHeaderRecord = false;

            int loopCounter = 1;            

            while (csvReader.Read())
            {
                if (loopCounter == rowCount) // this is the last row
                {
                    try
                    {
                        var newMarketPrice = new MarketPrice();

                        newMarketPrice.MarketPriceDate = DateTime.ParseExact(csvReader.GetField<string>(0), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        newMarketPrice.Price = csvReader.GetField<decimal>(1);
                                               
                        shareRun.MarketPrice = newMarketPrice;
                    }
                    catch (Exception)
                    {
                        throw new InvalidInputException("Invalid data in input on last row.");
                    }

                    break;
                }

                if (loopCounter == 1) // read first row
                {
                    if (!csvReader.TryGetField<int>(0, out numberOfDataRows))
                    {
                        throw new InvalidInputException("First line of input is not an integer.");
                    }                    
                }
                else
                {
                    try
                    {
                        var result = _recordFactory.GetRecord(csvReader.GetField<string>(0), csvReader);

                        if (result.GetType() == typeof(EmployeeShare))
                        {
                            shareRun.ShareRecords.Add((EmployeeShare)result);                            
                        }
                        else if (result.GetType() == typeof(EmployeeBonus))
                        {
                            shareRun.EmployeeBonuses.Add((EmployeeBonus)result);
                        }
                        else if (result.GetType() == typeof(EmployeeSale))
                        {
                            shareRun.SaleRecords.Add((EmployeeSale)result);
                        }
                    }
                    catch (InvalidInputException ex) // we dont want to eat up an exception lower in the stack
                    {
                        throw ex;
                    }
                    catch (Exception)
                    {
                        throw new InvalidInputException("Invalid data in input in share records.");
                    }
                }

                loopCounter++;
            }

            if (numberOfDataRows != shareRun.ShareRecords.Count + shareRun.EmployeeBonuses.Count + shareRun.SaleRecords.Count)
            {
                throw new InvalidInputException("Number of rows does not match count number in input.");
            }

            return shareRun;
        }
    }
}