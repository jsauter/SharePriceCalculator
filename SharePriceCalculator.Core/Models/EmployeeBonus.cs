using System;

namespace SharePriceCalculator.Core.Models
{
    public class EmployeeBonus : BaseRecord
    {
        public DateTime BonusDate { get; set; }
        public decimal Multiplier { get; set; }
    }
}