using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class PronounsController : Controller
    {
        private TranslateDb db;

        public PronounsController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index()
        {
            return View(db.Pronouns);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pronoun pronoun)
        {
            if (ModelState.IsValid)
            {
                if (pronoun != null)
                {
                    var find_pronoun = db.Pronouns.FirstOrDefault(w => w.PronounEng == pronoun.PronounEng);
                    var find_word = db.Words.FirstOrDefault(w => w.WordEng == pronoun.PronounEng);

                    if (find_pronoun == null)
                    {
                        db.Pronouns.Add(pronoun);

                        var word = new Word()
                        {
                            WordEng = pronoun.PronounEng,
                            WordRu = pronoun.PronounRu
                        };
                        db.Words.Add(word);
                        db.SaveChanges();
                    }
                    else
                    {
                        find_pronoun.PronounRu = pronoun.PronounRu;
                        db.Pronouns.Update(find_pronoun);
                        db.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(pronoun);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_pronoun = db.Pronouns.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_pronoun);
        }

        [HttpPost]
        public IActionResult Edit(Pronoun pronoun)
        {
            if (ModelState.IsValid)
            {
                var find_pronoun = db.Pronouns.FirstOrDefault(w => w.Id == pronoun.Id);

                if (find_pronoun != null)
                {
                    find_pronoun.PronounEng = pronoun.PronounEng;
                    find_pronoun.PronounRu = pronoun.PronounRu;
                    db.Pronouns.Update(find_pronoun);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pronoun);
        }

        public IActionResult Delete(string id)
        {
            var find_pronoun = db.Pronouns.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_pronoun != null)
            {
                db.Pronouns.Remove(find_pronoun);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}