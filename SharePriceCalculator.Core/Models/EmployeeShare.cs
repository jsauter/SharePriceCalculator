using System;

namespace SharePriceCalculator.Core.Models
{
    public class EmployeeShare : BaseRecord
    {
        public DateTime VestDate { get; set; }
        public int NumberOfUnits { get; set; }
        public decimal GrantPrice { get; set; }

        public decimal CalculateGain(MarketPrice marketPrice)
        {
            return CalculatePrice(marketPrice, 1.00M);
        }

        public decimal CalculateGain(MarketPrice marketPrice, EmployeeBonus employeeBonus)
        {
            if(employeeBonus != null)
                return CalculatePrice(marketPrice, employeeBonus.BonusDate > VestDate ? employeeBonus.Multiplier : 1.00M);

            return CalculatePrice(marketPrice, 1.00M);
            
        }

        private decimal CalculatePrice(MarketPrice marketPrice, decimal bonusMultiplier)
        {
            if (VestDate > marketPrice.MarketPriceDate ||
                            (marketPrice.Price - GrantPrice < 0.00M))
            {
                return 0.00M;
            }

            return Math.Round((marketPrice.Price * (NumberOfUnits * bonusMultiplier)) - (GrantPrice * (NumberOfUnits * bonusMultiplier)), 2, MidpointRounding.AwayFromZero);
        }

    }
}