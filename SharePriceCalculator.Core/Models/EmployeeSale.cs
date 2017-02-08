using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePriceCalculator.Core.Models
{
    public class EmployeeSale : BaseRecord
    {
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal MarketSalePrice { get; set; }

        public decimal CalculateSale(List<EmployeeShare> shares, List<EmployeeBonus> bonus)
        {
            var releventShares = shares.Where(x => x.VestDate <= SaleDate);

            var acb = releventShares.ToList().Sum(x => (x.NumberOfUnits * bonus[0].Multiplier)* x.GrantPrice) / releventShares.ToList().Sum(x => x.NumberOfUnits * bonus[0].Multiplier);

            return (Quantity*MarketSalePrice) - (Quantity*acb);
        }
    }
}