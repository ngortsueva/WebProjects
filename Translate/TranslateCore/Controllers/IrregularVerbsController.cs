using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class IrregularVerbsController : Controller
    {
        private TranslateDb db;

        public IrregularVerbsController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index(string filter = "all")
        {
            if (filter != "all")
            {
                return View(db.IrregularVerbs.Where(w => w.Infinitive.StartsWith(filter)));
            }
            return View(db.IrregularVerbs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IrregularVerb verb)
        {
            if (ModelState.IsValid)
            {
                var find_verb = db.IrregularVerbs.FirstOrDefault(v => v.Infinitive == verb.Infinitive);
                var find_word = db.Words.FirstOrDefault(w => w.WordEng == verb.Infinitive);

                if(find_verb == null)
                {
                    db.IrregularVerbs.Add(verb);

                    var word = new Word() {
                        WordEng = verb.Infinitive,
                        WordRu = verb.TranslateRu
                    };
                    db.Words.Add(word);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    find_verb.PastSimple = verb.PastSimple;
                    find_verb.PastParticiple = verb.PastParticiple;
                    find_verb.TranslateRu = verb.TranslateRu;
                    db.IrregularVerbs.Update(find_verb);                    
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(verb);
        }

        public IActionResult Edit(string id)
        {
            if (id == null || id == "") return null;

            var verb = db.IrregularVerbs.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(verb);
        }

        [HttpPost]
        public IActionResult Edit(IrregularVerb verb)
        {
            if (ModelState.IsValid)
            {
                db.IrregularVerbs.Update(verb);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(verb);
        }

        public IActionResult Delete(string id)
        {
            if (id == null || id == "") return null;

            var verb = db.IrregularVerbs.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if(verb != null)
            {
                db.IrregularVerbs.Remove(verb);
                db.SaveChanges();                
            }
            return RedirectToAction(nameof(Index));
        }
    }
}