using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class WordCategoryController : Controller
    {
        private TranslateDb db;

        public WordCategoryController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index(string filter = "all")
        {
            if (filter != "all")
            {
                return View(db.WordCategories                      
                    .Where(w => w.Name.StartsWith(filter))                    
                    .OrderBy(w=> w.Name)
                    );
            }
            return View(db.WordCategories.OrderBy(w => w.Name));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(WordCategory wordCategory)
        {
            if (ModelState.IsValid)
            {                
                var find_verb = db.WordCategories.FirstOrDefault(w => w.Name == wordCategory.Name);                    

                if (find_verb == null)
                {                    
                    db.WordCategories.Add(wordCategory);                        
                    db.SaveChanges();
                }                    
                return RedirectToAction(nameof(Index));                
            }
            return View(wordCategory);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_word = db.WordCategories.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_word);
        }

        [HttpPost]
        public IActionResult Edit(WordCategory wordCategory)
        {
            if (ModelState.IsValid)
            {
                db.WordCategories.Update(wordCategory);
                db.SaveChanges();                
                return RedirectToAction(nameof(Index));
            }
            return View(wordCategory);
        }

        public IActionResult Delete(string id)
        {
            var find_word = db.WordCategories.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_word != null)
            {
                db.WordCategories.Remove(find_word);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}