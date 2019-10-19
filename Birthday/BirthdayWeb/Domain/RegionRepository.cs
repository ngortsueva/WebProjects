using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class RegionRepository : IRegionRepository
    {
        private Birthday db;

        public IQueryable<Region> Regions { get { return db.Regions; } }

        public RegionRepository(Birthday injectDb) { db = injectDb; }

        public void SaveRegion(Region region)
        {
            if (region == null) return;

            if (region.Id == 0)
            {
                db.Regions.Add(region);
                db.SaveChanges();
            }
            else
            {
                Region find_region = db.Regions.FirstOrDefault(t => t.Id == region.Id);
                find_region.Name = region.Name;
                find_region.Country = region.Country;
                db.SaveChanges();
            }
        }

        public void DeleteRegion(Region region)
        {
            if (region == null) return;

            db.Regions.Remove(region);
            db.SaveChanges();
        }

        public void DeleteRegion(int id)
        {
            Region find_event = db.Regions.FirstOrDefault(t => t.Id == id);

            if (find_event == null) return;

            db.Regions.Remove(find_event);
            db.SaveChanges();
        }
    }
}
