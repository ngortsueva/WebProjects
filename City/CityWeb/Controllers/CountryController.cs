using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityWeb.Domain.Abstract;
using CityWeb.Domain.Entities;

namespace CityWeb.Controllers
{
    public class CountryController : Controller
    {
        private ICountryRepository repo;

        public CountryController(ICountryRepository repository)
        {
            repo = repository;
        }

        // GET: /<controller>/
        public IActionResult List(int continent)
        {
            ViewBag.WithHolidays = false;

            IEnumerable<Country> countries = repo.Countries
                .Where(c => c.Continent == continent);

            return View(countries);
        }

        public IActionResult Holidays(string countrycode)
        {
            Country c = repo.Countries
                .FirstOrDefault(x => x.Code == countrycode);

            if (c != null)
            {
                return View(c);
            }
            else return View();
        }

        public IActionResult Calendar(string calendarcode)
        {
            Country c = repo.Countries
                .FirstOrDefault(x => x.Code == calendarcode);

            if (c != null)
            {
                return View(c);
            }
            else return View();
        }
    }
}
