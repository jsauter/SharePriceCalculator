using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Services
{
    public class OutputRendererService : IOutputRendererService
    {
        public string GenerateOutput(MarketPrice marketPrice, List<EmployeeShare> employeeShares, List<EmployeeBonus> employeeBonuses, List<EmployeeSale> sales)
        {
            var mergedEmployeeShares = from e in employeeShares
                group e by new {e.EmployeeId}
                into g
                select new
                {
                    Id = g.Key.EmployeeId,
                    TotalVetted = g.Sum(p => p.CalculateGains(marketPrice, employeeBonuses.FirstOrDefault(x => x.EmployeeId == g.Key.EmployeeId), sales.Where(x => x.EmployeeId == g.Key.EmployeeId).Sum(x => x.Quantity))),
                    SalesTotal = sales.FirstOrDefault(x => x.EmployeeId == g.Key.EmployeeId) == null ? 0.00M : 
                                                                sales.Sum(x => x.CalculateSale(employeeShares.Where(y => y.EmployeeId == g.Key.EmployeeId).ToList(), 
                                                                employeeBonuses.Where(z => z.EmployeeId == g.Key.EmployeeId).ToList()))
                };
        
            var output = new StringBuilder();

            foreach (var employeeShare in mergedEmployeeShares)
            {
                output.Append(employeeShare.Id);
                output.Append(",");
                output.Append(employeeShare.TotalVetted);
                output.Append(",");
                output.Append(employeeShare.SalesTotal);                
                output.Append(Environment.NewLine);
            }

            return output.ToString();
        }
    }
}