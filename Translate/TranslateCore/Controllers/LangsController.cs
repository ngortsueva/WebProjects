using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class LangsController : Controller
    {
        private TranslateDb db;

        public LangsController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index()
        {
            return View(db.Languages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Language language)
        {
            if(language != null)
            {
                var lang = db.Languages.FirstOrDefault(l => l.Name == language.Name);

                if(lang == null)
                {
                    db.Languages.Add(language);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(language);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var lang = db.Languages.FirstOrDefault(l => l.Id == Convert.ToInt32(id));

            return View(lang);
        }

        [HttpPost]
        public IActionResult Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                db.Languages.Update(language);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        public IActionResult Delete(string id)
        {
            var lang = db.Languages.FirstOrDefault(l => l.Id == Convert.ToInt32(id));

            if(lang != null)
            {
                db.Languages.Remove(lang);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}