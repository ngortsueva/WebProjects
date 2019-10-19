using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface ICountryRepository
    {
        IQueryable<Country> Countries { get; }
        void SaveCountry(Country country);
        void DeleteCountry(Country country);
        void DeleteCountry(int id);
    }
}
