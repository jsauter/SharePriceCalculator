using System.Collections.Generic;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Services
{
    public interface IOutputRendererService
    {
        string GenerateOutput(MarketPrice marketPrice, List<EmployeeShare> employeeShares, List<EmployeeBonus> employeeBonuses);
    }
}