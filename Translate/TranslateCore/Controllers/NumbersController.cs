using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class NumbersController : Controller
    {
        private TranslateDb db;

        public NumbersController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index()
        {
            return View(db.Numbers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Number number)
        {
            if (ModelState.IsValid)
            {
                if (number != null)
                {
                    var find_num = db.Numbers.FirstOrDefault(w => w.Word == number.Word);
                    var find_word = db.Words.FirstOrDefault(w => w.WordEng == number.Word);

                    if (find_num == null)
                    {
                        db.Numbers.Add(number);

                        var word = new Word()
                        {
                            WordEng = number.Word,
                            WordRu = number.WordRu
                        };
                        db.Words.Add(word);
                        db.SaveChanges();
                    }
                    else
                    {
                        find_num.WordRu = number.WordRu;
                        db.Numbers.Update(find_num);
                        db.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(number);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_num = db.Numbers.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_num);
        }

        [HttpPost]
        public IActionResult Edit(Number number)
        {
            if (ModelState.IsValid)
            {
                var find_num = db.Numbers.FirstOrDefault(w => w.Id == number.Id);

                if (find_num != null)
                {
                    find_num.Word = number.Word;
                    find_num.WordRu = number.WordRu;
                    db.Numbers.Update(find_num);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(number);
        }

        public IActionResult Delete(string id)
        {
            var find_num = db.Numbers.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_num != null)
            {
                db.Numbers.Remove(find_num);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}