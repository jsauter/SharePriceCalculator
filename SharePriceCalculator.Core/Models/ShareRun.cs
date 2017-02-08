using System.Collections;
using System.Collections.Generic;

namespace SharePriceCalculator.Core.Models
{
    public class ShareRun
    {
        public List<EmployeeSale> SaleRecords { get; set; }
        public List<EmployeeShare> ShareRecords { get; set; }
        public List<EmployeeBonus> EmployeeBonuses { get; set; }
        public MarketPrice MarketPrice { get; set; }
        
        public ShareRun()
        {
            ShareRecords = new List<EmployeeShare>();
            EmployeeBonuses = new List<EmployeeBonus>();
            SaleRecords = new List<EmployeeSale>();
        }
    }
}