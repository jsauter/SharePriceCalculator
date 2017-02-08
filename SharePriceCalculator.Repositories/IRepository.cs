using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SharePriceCalculator.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}