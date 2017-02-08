using System;

namespace SharePriceCalculator.Core.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        {
            
        }
    }
}