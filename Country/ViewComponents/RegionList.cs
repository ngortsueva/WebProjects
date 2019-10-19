using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryWeb.Domain;

namespace CountryWeb.ViewComponents
{
    public class RegionList : ViewComponent
    {
        private CountryDb db;

        public RegionList(CountryDb injectDb) { db = injectDb; }

        public IViewComponentResult Invoke(string countryId)
        {
            if (countryId == null) return null;

            return View(db.Regions.Where(r => r.Country.Id == Convert.ToInt32(countryId)));
        }
    }
}
