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
    public class ContinentList : ViewComponent
    {
        private CountryDb db;

        public ContinentList(CountryDb injectDb) { db = injectDb; }

        public IViewComponentResult Invoke()
        {
            return View(db.Continents);
        }
    }
}
