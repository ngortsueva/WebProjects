using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class VerbsController : Controller
    {
        private TranslateDb db;

        public VerbsController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index()
        {
            return View(db.Verbs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Verb verb)
        {
            if (ModelState.IsValid)
            {
                if (verb != null)
                {
                    var find_verb = db.Verbs.FirstOrDefault(w => w.VerbEng == verb.VerbEng);
                    var find_word = db.Words.FirstOrDefault(w => w.WordEng == verb.VerbEng);

                    if (find_verb == null)
                    {
                        db.Verbs.Add(verb);

                        var word = new Word()
                        {
                            WordEng = verb.VerbEng,
                            WordRu = verb.VerbRu
                        };
                        db.Words.Add(word);
                        db.SaveChanges();
                    }
                    else
                    {
                        find_verb.VerbRu = verb.VerbRu;
                        db.Verbs.Update(find_verb);
                        db.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(verb);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_verb = db.Verbs.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_verb);
        }

        [HttpPost]
        public IActionResult Edit(Verb word)
        {
            if (ModelState.IsValid)
            {
                var find_verb = db.Verbs.FirstOrDefault(w => w.Id == word.Id);

                if (find_verb != null)
                {
                    find_verb.VerbEng = word.VerbEng;
                    find_verb.VerbRu = word.VerbRu;
                    db.Verbs.Update(find_verb);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        public IActionResult Delete(string id)
        {
            var find_verb = db.Verbs.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_verb != null)
            {
                db.Verbs.Remove(find_verb);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}