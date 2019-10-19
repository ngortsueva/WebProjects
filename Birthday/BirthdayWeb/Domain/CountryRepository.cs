using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class CountryRepository : ICountryRepository
    {
        private Birthday db;

        public IQueryable<Country> Countries { get { return db.Countries; } }

        public CountryRepository(Birthday injectDb) { db = injectDb; }

        public void SaveCountry(Country country)
        {
            if (country == null) return;

            if (country.Id == 0)
            {
                db.Countries.Add(country);
                db.SaveChanges();
            }
            else
            {
                Country find_event = db.Countries.FirstOrDefault(t => t.Id == country.Id);
                find_event.Name = country.Name;                
                db.SaveChanges();
            }
        }

        public void DeleteCountry(Country country)
        {
            if (country == null) return;

            db.Countries.Remove(country);
            db.SaveChanges();
        }

        public void DeleteCountry(int id)
        {
            Country find_event = db.Countries.FirstOrDefault(t => t.Id == id);

            if (find_event == null) return;

            db.Countries.Remove(find_event);
            db.SaveChanges();
        }
    }
}
