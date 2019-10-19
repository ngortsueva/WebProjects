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
    public class ContinentController : Controller
    {
        private CountryDb db;

        public ContinentController(CountryDb injectDb) { db = injectDb; }

        public IActionResult Index(string id)
        {
            ViewBag.SelectedContinent = id;
            return View(db.Continents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Continent continent)
        {
            if (ModelState.IsValid)
            {
                db.Continents.Add(continent);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(continent);
        }

        public IActionResult Edit(string id)
        {
            var continent = db.Continents.FirstOrDefault(c => c.Id == Convert.ToInt32(id));
            return View(continent);
        }

        [HttpPost]
        public IActionResult Edit(Continent continent)
        {
            if (ModelState.IsValid)
            {
                db.Continents.Update(continent);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(continent);
        }

        public IActionResult Delete(string id)
        {
            var continent = db.Continents.FirstOrDefault(c => c.Id == Convert.ToInt32(id));

            if(continent != null)
            {
                db.Continents.Remove(continent);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}