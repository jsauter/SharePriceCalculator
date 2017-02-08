using System.Collections.Generic;
using System.Security.Policy;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Repositories
{
    public class InputRepository : IRepository<ShareRecord>
    {
        public IEnumerable<ShareRecord> GetAll()
        {
            var shareList = new List<ShareRecord>();

            //var csvHelper = new CsvHelper.CsvReader();

            return shareList;
        }
    }
}