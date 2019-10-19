using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CountryWeb.Domain;
using CountryWeb.Models;
using CountryWeb.ViewModels;

namespace CountryWeb.ViewComponents
{
    public class CountryTable : ViewComponent
    {
        private CountryDb db;

        public CountryTable(CountryDb injectDb) { db = injectDb; }

        public IViewComponentResult Invoke(string continentId)
        {
            if (continentId == "All") return View(db.Countries);

            return View(db.Countries
                            .Where(c => c.Continent.Id == Convert.ToInt32(continentId))
                        );
        }
    }
}
