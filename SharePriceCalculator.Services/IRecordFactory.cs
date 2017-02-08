using System.Security.Cryptography.X509Certificates;
using CsvHelper;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Services
{
    public interface IRecordFactory
    {
        BaseRecord GetRecord(string type, CsvReader csvReader);
    }
}