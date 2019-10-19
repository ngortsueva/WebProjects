using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class ColorsController : Controller
    {
        private TranslateDb db;

        public ColorsController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index()
        {            
            return View(db.Colors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Color color)
        {
            if (ModelState.IsValid)
            {
                if (color != null)
                {
                    var find_color = db.Colors.FirstOrDefault(w => w.ColorEng == color.ColorEng);
                    var find_word = db.Words.FirstOrDefault(w => w.WordEng == color.ColorEng);

                    if (find_color == null)
                    {
                        db.Colors.Add(color);

                        var word = new Word()
                        {
                            WordEng = color.ColorEng,
                            WordRu = color.ColorRu
                        };
                        db.Words.Add(word);
                        db.SaveChanges();
                    }
                    else
                    {
                        find_color.ColorRu = color.ColorRu;
                        db.Colors.Update(find_color);
                        db.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(color);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_pronoun = db.Colors.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_pronoun);
        }

        [HttpPost]
        public IActionResult Edit(Color word)
        {
            if (ModelState.IsValid)
            {
                var find_pronoun = db.Colors.FirstOrDefault(w => w.Id == word.Id);

                if (find_pronoun != null)
                {
                    find_pronoun.ColorEng = word.ColorEng;
                    find_pronoun.ColorRu = word.ColorRu;
                    db.Colors.Update(find_pronoun);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        public IActionResult Delete(string id)
        {
            var find_pronoun = db.Colors.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_pronoun != null)
            {
                db.Colors.Remove(find_pronoun);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
