using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityWeb.Domain.Abstract;
using CityWeb.Domain.Entities;

namespace CityWeb.Domain.Concrete
{
    public class CityRepositories : ICityRepository
    {
        private Citydb db = new Citydb();

        public IQueryable<City> Cities
        {
            get { return db.Cities; }
        }

        public void SaveCity(City city)
        {
            if (city.Id == 0)
            {
                db.Cities.Add(city);
            }
            else
            {
                City find_c = db.Cities.FirstOrDefault(c => c.Id == city.Id);
                find_c.Name = city.Name;
            }
            db.SaveChanges();
        }

        public void DeleteCity(City city)
        {
            db.Cities.Remove(city);
            db.SaveChanges();
        }

        public void Print()
        {
            foreach (City item in db.Cities)
            {
                Console.WriteLine(item);
            }
        }
    }
}
