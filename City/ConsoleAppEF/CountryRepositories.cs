using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEF
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
            if (country.id == 0)
            {
                db.Countries.Add(country);
            }
            else
            {
                Country find_c = db.Countries.FirstOrDefault(c => c.id == country.id);
                find_c.name = country.name;
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
