using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class WordTypesController : Controller
    {
        private TranslateDb db;

        public WordTypesController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index()
        {
            return View(db.WordTypes.OrderBy(w => w.Name));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(WordType wordType)
        {
            if (ModelState.IsValid)
            {
                var find_verb = db.WordTypes.FirstOrDefault(w => w.Name == wordType.Name);                    

                if (find_verb == null)
                {
                    db.WordTypes.Add(wordType);                       
                    db.SaveChanges();
                }                    
                return RedirectToAction(nameof(Index));                
            }
            return View(wordType);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_verb = db.WordTypes.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_verb);
        }

        [HttpPost]
        public IActionResult Edit(WordType wordType)
        {
            if (ModelState.IsValid)
            {
                db.WordTypes.Update(wordType);
                db.SaveChanges();                
                return RedirectToAction(nameof(Index));
            }
            return View(wordType);
        }

        public IActionResult Delete(string id)
        {
            var find_verb = db.WordTypes.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_verb != null)
            {
                db.WordTypes.Remove(find_verb);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}