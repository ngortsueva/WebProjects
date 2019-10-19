using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityWeb.Domain.Abstract;
using CityWeb.Domain.Entities;

namespace CityWeb.Domain.Concrete
{
    public class ContinentRepository : IContinentRepository
    {
        private Citydb db = new Citydb();

        public IQueryable<Continent> Continents
        {
            get { return db.Continents; }
        }

        public void SaveContinent(Continent continent)
        {
            if (continent.Id == 0)
            {
                db.Continents.Add(continent);
            }
            else
            {
                Continent find_c = db.Continents.FirstOrDefault(c => c.Id == continent.Id);
                find_c.Name = continent.Name;
            }
            db.SaveChanges();
        }

        public void DeleteContinent(Continent continent)
        {
            db.Continents.Remove(continent);
            db.SaveChanges();
        }

        public void Print()
        {
            foreach (Continent item in db.Continents)
            {
                Console.WriteLine(item);
            }
        }
    }
}
