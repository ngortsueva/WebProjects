using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CountryWeb.Domain;
using CountryWeb.Models;
using CountryWeb.ViewModels;

namespace CountryWeb.Controllers
{
    public class CountryController : Controller
    {
        private CountryDb db;

        public CountryController(CountryDb countryDb) { db = countryDb; }

        public IActionResult Index(string id)
        {
            ViewBag.SelectedCountry = id;
            return View(db.Countries);
        }

        public IActionResult Index(string country, string region)
        {
            ViewBag.SelectedCountry = country;
            ViewBag.SelectedRegion = region;
            return View(db.Countries);
        }

        public IActionResult Create()
        {
            var continents = new SelectList(db.Continents.Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");
            var viewModel = new CountryViewModel
            {
                Continents = continents
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CountryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Country.Continent = db.Continents.FirstOrDefault(c => c.Id == viewModel.SelectedContinent);
                db.Countries.Add(viewModel.Country);
                db.SaveChanges();
                return RedirectToAction("Index", "Continent", new { id = "All" });
            }
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var continents = new SelectList(db.Continents.Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var country = db.Countries
                                .Include(c => c.Continent)
                                .FirstOrDefault(c => c.Id == Convert.ToInt32(id));

            var viewModel = new CountryViewModel
            {
                Continents = continents,
                Country = country,
                SelectedContinent = country.Continent?.Id
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CountryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Country.Continent = db.Continents.FirstOrDefault(c => c.Id == viewModel.SelectedContinent);
                db.Countries.Update(viewModel.Country);
                db.SaveChanges();
                return RedirectToAction("Index", "Continent", new { id = "All" });
            }
            return View(viewModel);
        }

        public IActionResult Delete(string id)
        {
            var country = db.Countries.FirstOrDefault(c => c.Id == Convert.ToInt32(id));

            if (country != null)
            {
                db.Countries.Remove(country);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Continent", new { id = "All" });
        }
    }
}