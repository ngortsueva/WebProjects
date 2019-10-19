using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityWeb.Domain.Abstract;
using CityWeb.Domain.Entities;

namespace CityWeb.Controllers
{
    public class CityController : Controller
    {
        private ICityRepository repo;

        public CityController(ICityRepository repository)
        {
            repo = repository;
        }

        public IActionResult List(int cityid)
        {
            IEnumerable<City> cities = repo.Cities
                .Where(c => c.Id == cityid);

            return View(cities);
        }
    }
}