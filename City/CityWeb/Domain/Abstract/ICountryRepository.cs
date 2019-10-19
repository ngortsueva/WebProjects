using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityWeb.Domain.Entities;

namespace CityWeb.Domain.Abstract
{
    public interface ICountryRepository
    {
        IQueryable<Country> Countries { get; }
        void SaveCountry(Country country);
        void DeleteCountry(Country country);
        void Print();
    }
}
