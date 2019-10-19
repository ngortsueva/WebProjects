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
    public class WordSubCategoryController : Controller
    {
        private TranslateDb db;

        public WordSubCategoryController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index(string categoryId = "")
        {
            if (categoryId != "" && categoryId != "all")
            {
                var cat = db.WordCategories.FirstOrDefault(w => w.Id == Convert.ToInt32(categoryId));

                ViewBag.categoryId = cat.Id.ToString();
                ViewBag.categoryName = cat.Name;


                return View(db.WordSubCategories
                    .Include(w => w.Category)
                    .Where(w => w.Category.Id == cat.Id)
                    .OrderBy(w => w.Name)
                    );
            }

            return View(db.WordSubCategories
                .Include(w => w.Category)
                .OrderBy(w => w.Name));
        }

        [HttpGet]
        public IActionResult Create(string categoryId)
        {
            if(categoryId != "")
            {
                var cat = db.WordCategories.FirstOrDefault(w => w.Id == Convert.ToInt32(categoryId));

                return View(new WordSubCategory() { Category = cat });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(WordSubCategory wordSubCategory)
        {
            if (ModelState.IsValid)
            {
                var find_word = db.WordSubCategories.FirstOrDefault(w => w.Name == wordSubCategory.Name);
                

                if (find_word == null)
                {
                    var category = db.WordCategories.FirstOrDefault(w => w.Id == wordSubCategory.Category.Id);
                    wordSubCategory.Category = category;

                    db.WordSubCategories.Add(wordSubCategory);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index), new { categoryId = wordSubCategory.Category.Id.ToString() });
            }
            return View(wordSubCategory);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_word = db.WordSubCategories
                .Include(w => w.Category)
                .FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_word);
        }

        [HttpPost]
        public IActionResult Edit(WordSubCategory wordSubCategory)
        {
            if (ModelState.IsValid)
            {
                var cat = db.WordCategories.FirstOrDefault(w => w.Id == wordSubCategory.Category.Id);
                wordSubCategory.Category = cat;

                db.WordSubCategories.Update(wordSubCategory);
                db.SaveChanges();
                return RedirectToAction(nameof(Index), new { categoryId = wordSubCategory.Category.Id.ToString() });
            }
            return View(wordSubCategory);
        }

        public IActionResult Delete(string id)
        {
            var find_word = db.WordSubCategories
                .Include(w => w.Category)
                .FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            string catId = "";
            
            if (find_word != null)
            {
                catId = find_word.Category.Id.ToString();

                db.WordSubCategories.Remove(find_word);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index), new { categoryId = catId } );
        }


    }
}