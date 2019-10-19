using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEFCore
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
            if (continent.id == 0)
            {
                db.Continents.Add(continent);
            }
            else
            {
                Continent find_c = db.Continents.FirstOrDefault(c => c.id == continent.id);
                find_c.name = continent.name;
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
