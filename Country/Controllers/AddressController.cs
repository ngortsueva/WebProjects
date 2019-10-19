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
    public class AddressController : Controller
    {
        private CountryDb db;

        public AddressController(CountryDb injectDb) { db = injectDb; }

        public IActionResult Index()
        {
            var addressList = db.Addresses
                .Include(a => a.Street)
                .Include(a => a.City)
                .Include(a => a.Region)
                .Include(a => a.Country);

            return View(addressList);
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

            int cityId = Convert.ToInt32(cities.First().Value);

            var streets = new SelectList(db.Streets
                .Where(s => s.City.Id == cityId)
                .Select(s => new { Id = s.Id, Name = s.Name }).ToList(), "Id", "Name");

            var viewModel = new AddressViewModel
            {
                Countries = countries,
                Regions = regions,
                Cities = cities,
                Streets = streets
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var address = viewModel.Address;
                address.Country = db.Countries.FirstOrDefault(c => c.Id == viewModel.SelectedCountry);
                address.Region = db.Regions.FirstOrDefault(r => r.Id == viewModel.SelectedRegion);
                address.City = db.Cities.FirstOrDefault(c => c.Id == viewModel.SelectedCity);
                address.Street = db.Streets.FirstOrDefault(s => s.Id == viewModel.SelectedStreet);

                db.Addresses.Add(address);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var address = db.Addresses
                .Include(c => c.Country)
                .Include(r => r.Region)
                .Include(c => c.City)
                .Include(s => s.Street)
                .FirstOrDefault(a => a.Id == Convert.ToInt32(id));

            var countries = new SelectList(db.Countries
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var regions = new SelectList(db.Regions
                .Where(r => r.Country.Id == address.Country.Id)
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var cities = new SelectList(db.Cities
                .Where(c => c.Region.Id == address.Region.Id)
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var streets = new SelectList(db.Streets
                .Where(s => s.City.Id == address.City.Id)
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");

            var viewModel = new AddressViewModel
            {
                Countries = countries,
                Regions = regions,
                Cities = cities,
                Streets = streets,
                SelectedCountry = address.Country.Id,
                SelectedRegion = address.Region.Id,
                SelectedCity = address.City.Id,
                SelectedStreet = address.Street.Id,
                Address = address
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddressViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var address = viewModel.Address;
                address.Country = db.Countries.FirstOrDefault(c => c.Id == viewModel.SelectedCountry);
                address.Region = db.Regions.FirstOrDefault(r => r.Id == viewModel.SelectedRegion);
                address.City = db.Cities.FirstOrDefault(c => c.Id == viewModel.SelectedCity);
                address.Street = db.Streets.FirstOrDefault(s => s.Id == viewModel.SelectedStreet);

                db.Addresses.Update(address);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Delete(string id)
        {
            var address = db.Addresses.FirstOrDefault(a => a.Id == Convert.ToInt32(id));

            if(address != null)
            {
                db.Addresses.Remove(address);
                db.SaveChanges();                
            }
            return RedirectToAction(nameof(Index));
        }
    }
}