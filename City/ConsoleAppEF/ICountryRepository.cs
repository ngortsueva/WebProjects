using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEF
{
    public interface ICountryRepository
    {
        IQueryable<Country> Countries { get; }
        void SaveCountry(Country country);
        void DeleteCountry(Country country);
        void Print();
    }
}
