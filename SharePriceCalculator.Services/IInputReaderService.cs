using System.Collections;
using System.Collections.Generic;
using SharePriceCalculator.Core.Models;

namespace SharePriceCalculator.Services
{
    public interface IInputReaderService
    {
        ShareRun ReadInput(string inputValue);
    }
}