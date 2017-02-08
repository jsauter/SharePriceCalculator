using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePriceCalculator.Core.Models
{
    public class EmployeeShare : BaseRecord
    {
        public DateTime VestDate { get; set; }
        public int NumberOfUnits { get; set; }
        public decimal GrantPrice { get; set; }

        public decimal CalculateGains(MarketPrice marketPrice)
        {
            return CalculatePrice(marketPrice, 1.00M, NumberOfUnits);
        }
    
        public decimal CalculateGains(MarketPrice marketPrice, EmployeeBonus employeeBonus, int soldUnits)
        {
            if(employeeBonus != null)
                return CalculatePrice(marketPrice, GetMultiplier(employeeBonus, marketPrice), NumberOfUnits - soldUnits);

            return CalculatePrice(marketPrice, 1.00M, NumberOfUnits - soldUnits);            
        }

        private decimal CalculatePrice(MarketPrice marketPrice, decimal bonusMultiplier, int numberOfUnits)
        {
            if (VestDate > marketPrice.MarketPriceDate ||
                            (marketPrice.Price - GrantPrice < 0.00M))
            {
                return 0.00M;
            }

            return Math.Round((marketPrice.Price * (numberOfUnits * bonusMultiplier)) - (GrantPrice * (numberOfUnits * bonusMultiplier)), 2, MidpointRounding.AwayFromZero);
        }

        private decimal GetMultiplier(EmployeeBonus employeeBonus, MarketPrice marketPrice)
        {
            return (employeeBonus.BonusDate > VestDate && employeeBonus.BonusDate <= marketPrice.MarketPriceDate ? employeeBonus.Multiplier : 1.00M);
        }

    }
}