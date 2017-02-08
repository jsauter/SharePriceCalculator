using System;

namespace SharePriceCalculator.Core.Models
{
    public class EmployeeSale : BaseRecord
    {
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal MarketSalePrice { get; set; }
    }
}