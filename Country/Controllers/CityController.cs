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
    public class CityController : Controller
    {
        private CountryDb db;

        public CityController(CountryDb countryDb) { db = countryDb; }

        public IActionResult Index()
        {
            var cities = db.Cities
                            .Include(c => c.Country)
                            .Include(c => c.Region);                               
            return View(cities);
        }

        public JsonResult StreetList(string cityId)
        {
            var cities = db.Streets.Where( s => s.City.Id == Convert.ToInt32(cityId))
                .Select(c => new { c.Id, c.Name });

            return Json(JsonConvert.SerializeObject(cities));
        }


        public JsonResult CityList(string regionId)
        {
            var cities = db.Cities.Where(c => c.Region.Id == Convert.ToInt32(regionId))
                .Select(c => new { c.Id, c.Name });

            return Json(JsonConvert.SerializeObject(cities));
        }

        public JsonResult RegionList(string countryId)
        {
            var regions = db.Regions.Where(r => r.Country.Id == Convert.ToInt32(countryId))
                .Select(r => new { r.Id, r.Name });

            return Json(JsonConvert.SerializeObject(regions));
        }

        public IActionResult Create()
        {
            var countries = new SelectList(db.Countries.Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            int countryId = Convert.ToInt32(countries.First().Value);

            var regions = new SelectList(db.Regions
               .Where(r => r.Country.Id == countryId)
               .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var viewModel = new CityViewModel
            {
                Countries = countries,
                Regions = regions
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                City city = viewModel.City;
                city.Country = db.Countries.FirstOrDefault(c => c.Id == viewModel.SelectedCountry);
                city.Region = db.Regions.FirstOrDefault(r => r.Id == viewModel.SelectedRegion);  
                
                db.Cities.Add(city);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            City city = db.Cities
                .Include(c=>c.Country)
                .Include(c=>c.Region)
                .FirstOrDefault(c => c.Id == Convert.ToInt32(id));

            var countries = new SelectList(db.Countries.Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var regions = new SelectList(db.Regions
                .Where(r => r.Country.Id == city.Country.Id)
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var viewModel = new CityViewModel {
                City = city,
                Countries = countries,
                Regions = regions,  
                SelectedCountry = city.Country.Id,
                SelectedRegion = city.Region.Id
            };
                
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                City city = viewModel.City;
                city.Country = db.Countries.FirstOrDefault(c => c.Id == viewModel.SelectedCountry);
                city.Region = db.Regions.FirstOrDefault(r => r.Id == viewModel.SelectedRegion);

                db.Cities.Update(city);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Delete(string id)
        {
            var city = db.Cities.FirstOrDefault(c => c.Id == Convert.ToInt32(id));

            if(city != null)
            {
                db.Cities.Remove(city);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}