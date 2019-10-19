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
    public class RegionController : Controller
    {
        private CountryDb db;

        public RegionController(CountryDb countryDb) { db = countryDb; }

        public IActionResult Index()
        {
            var regions = db.Regions
                                .Include(r => r.Country);
            return View(regions);
        }

        public IActionResult Create()
        {
            var countries = new SelectList(db.Countries.Select(c => new { Id = c.Id, Name = c.Name }).ToList(), "Id", "Name");
            var viewModel = new RegionViewModel
            {
                Countries = countries
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(RegionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Region region = viewModel.Region;
                Country country = db.Countries.FirstOrDefault(c => c.Id == viewModel.SelectedCountry);

                region.Country = country;

                db.Regions.Add(region);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var region = db.Regions                            
                            .Include(r => r.Country) 
                            .FirstOrDefault(r => r.Id == Convert.ToInt32(id));

            return View(region);
        }

        [HttpPost]
        public IActionResult Edit(Region region)
        {
            if (ModelState.IsValid)
            {
                db.Regions.Update(region);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        public IActionResult Delete(string id)
        {
            var region = db.Regions.FirstOrDefault(c => c.Id == Convert.ToInt32(id));

            if (region != null)
            {
                db.Regions.Remove(region);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}