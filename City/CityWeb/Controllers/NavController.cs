using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityWeb.Domain.Abstract;
using CityWeb.Domain.Entities;

namespace CityWeb.Controllers
{
    public class NavController : Controller
    {
        private IContinentRepository repo;

        public NavController(IContinentRepository repository)
        {
            repo = repository;
        }

        // GET: Nav
        public PartialViewResult Menu(string continent = null)
        {
            ViewBag.SelectedContinent = continent;
            IEnumerable<Continent> continents = repo.Continents
                .OrderBy(x => x.Id);

            return PartialView(continents);
        }
    }
}