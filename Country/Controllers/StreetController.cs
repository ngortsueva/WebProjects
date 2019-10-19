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

namespace CountryWeb.Controllers
{
    public class StreetController : Controller
    {
        private CountryDb db;

        public StreetController(CountryDb countryDb) { db = countryDb; }

        public IActionResult Index()
        {
            var streets = db.Streets
                .Include(s => s.City)
                    .ThenInclude(s => s.Region)
                        .ThenInclude(s => s.Country);            

            return View(streets);
        }

        public IActionResult Create()
        {
            var countries = new SelectList(db.Countries.Select(c => new { c.Id, c.Name }).ToList(), "Id", "Name");

            int countryId = Convert.ToInt32(countries.First().Value);

            var regions = new SelectList(db.Regions
               .Where(r => r.Country.Id == countryId)
               .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            int regionId = Convert.ToInt32(regions.First().Value);

            var cities = new SelectList(db.Cities
                .Where(c => c.Region.Id == regionId)
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var viewModel = new StreetViewModel
            {
                Countries = countries,
                Regions = regions,
                Cities = cities                
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(StreetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var street = viewModel.Street;
                street.City = db.Cities.FirstOrDefault(c => c.Id == viewModel.SelectedCity);

                db.Streets.Add(street);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var street = db.Streets
                .Include(s => s.City)
                    .ThenInclude(r => r.Region)
                        .ThenInclude(c => c.Country)
                .FirstOrDefault(s => s.Id == Convert.ToInt32(id));

            var countries = new SelectList(db.Countries
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var regions = new SelectList(db.Regions
                .Where(r => r.Country.Id == street.City.Country.Id)
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var cities = new SelectList(db.Cities
                .Where(c => c.Region.Id == street.City.Region.Id)
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var viewModel = new StreetViewModel
            {                
                Countries = countries,
                Regions = regions,
                Cities = cities,
                SelectedCountry = street.City.Country.Id,
                SelectedRegion = street.City.Region.Id,
                SelectedCity = street.City.Id,
                Street = street
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StreetViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Street street = viewModel.Street;
                street.City = db.Cities.FirstOrDefault(c => c.Id == viewModel.SelectedCity);

                db.Streets.Update(street);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Delete(string id)
        {
            var street = db.Streets.FirstOrDefault(s => s.Id == Convert.ToInt32(id));

            if(street != null)
            {
                db.Streets.Remove(street);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}