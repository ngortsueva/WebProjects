using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityWeb.Domain.Abstract;
using CityWeb.Domain.Entities;

namespace CityWeb.Domain.Concrete
{
    public class CountryRepositories : ICountryRepository
    {
        private Citydb db = new Citydb();

        public IQueryable<Country> Countries
        {
            get { return db.Countries; }
        }

        public void SaveCountry(Country country)
        {
            if (country.Id == 0)
            {
                db.Countries.Add(country);
            }
            else
            {
                Country find_c = db.Countries.FirstOrDefault(c => c.Id == country.Id);
                find_c.Name = country.Name;
            }
            db.SaveChanges();
        }

        public void DeleteCountry(Country country)
        {
            db.Countries.Remove(country);
            db.SaveChanges();
        }

        public void Print()
        {
            foreach (Country item in db.Countries)
            {
                Console.WriteLine(item);
            }
        }
    }
}
