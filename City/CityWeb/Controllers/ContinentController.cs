using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityWeb.Domain.Abstract;
using CityWeb.Domain.Entities;

namespace CityWeb.Controllers
{
    public class ContinentController : Controller
    {
        private IContinentRepository repo;

        public ContinentController(IContinentRepository repository)
        {
            repo = repository;
        }

        public IActionResult List()
        {
            IEnumerable<Continent> continents = repo.Continents;

            return View(continents);
        }
    }
}