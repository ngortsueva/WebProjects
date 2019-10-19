using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.Controllers
{
    public class DictionaryController : Controller
    {
        private TranslateDb db;

        public DictionaryController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index(string filter = "all")
        {
            /*
            var result = from words in db.Words
                         join types in db.WordTypes on words.WordType.Id equals types.Id into t2
                         from types in t2.DefaultIfEmpty()
                         group types by new { types.Id } into wordTypes
                         select new {                             
                             TypesId = wordTypes.Key.Id,
                             TypesCount = wordTypes.Count(x => x.Id != 0)                            
                         };
            */
            if(filter != "all")
            {
                Response.Cookies.Append("filter", filter);
                return View(db.Words.Where(w => w.WordEng.StartsWith(filter)));
            }
            Response.Cookies.Delete("filter");
            return View(db.Words);
        }

        public IActionResult Index2()
        {
            var result = from words in db.Words
                         join types in db.WordTypes on words.WordType.Id equals types.Id into t2
                         from defValue in t2.DefaultIfEmpty()
                         select new
                         {
                             Id = words.Id,
                             WordEng = words.WordEng,
                             WordRu = words.WordRu,
                             WordType = defValue.Name
                         };

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Word word)
        {
            if (ModelState.IsValid)
            {
                if (word != null)
                {
                    var find_word = db.Words.FirstOrDefault(w => w.WordEng == word.WordEng);

                    if (find_word == null)
                    {
                        db.Words.Add(word);
                        db.SaveChanges();
                    }
                    else
                    {
                        find_word.WordRu = word.WordRu;
                        db.Words.Update(find_word);
                        db.SaveChanges();
                    }
                    return RedirectToAction(nameof(Index), new { filter = Request.Cookies["filter"]});
                }
            }
            return View(word);           
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var find_word = db.Words.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_word);
        }

        [HttpPost]
        public IActionResult Edit(Word word)
        {
            if (ModelState.IsValid)
            {
                var find_word = db.Words.FirstOrDefault(w => w.Id == word.Id);

                if(find_word != null)
                {
                    find_word.WordEng = word.WordEng;
                    find_word.WordRu = word.WordRu;
                    db.Words.Update(find_word);
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        public IActionResult Delete(string id)
        {
            var find_word = db.Words.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if(find_word != null)
            {
                db.Words.Remove(find_word);
                db.SaveChanges();                
            }
            return RedirectToAction(nameof(Index));
        }

        public IEnumerable<Word> IsExist(string word)
        {
            return db.Words.Where(w => w.WordEng.StartsWith(word));

        }

        [HttpGet]
        public IActionResult AddWord()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddType(string wordId)
        {
            ViewBag.wordId = wordId;
            return View(db.WordTypes.OrderBy(t => t.Name));
        }

        [HttpPost]
        public string AddType(string word, string type)
        {
            if (word == null || type == null) return "BAD";

            var find_word = db.Words.FirstOrDefault(w => w.Id == Convert.ToInt32(word));

            var find_type = db.WordTypes.FirstOrDefault(w => w.Id == Convert.ToInt32(type));

            if(find_word != null && find_type != null)
            {
                //find_word.WordType = find_type;
                //db.Words.Update(find_word);
                //db.SaveChanges();
                return JsonConvert.SerializeObject( new {
                    wordId = word,
                    typeId = find_type.Id,
                    typeName = find_type.Name
                });
            }

            return "BAD";
        }

        public IActionResult AddCategory(string wordId)
        {
            ViewBag.wordId = wordId;
            return View(db.WordCategories.OrderBy(t => t.Name));
        }

        [HttpPost]
        public string AddCategory(string word, string category)
        {
            if (word == null || category == null) return "BAD";

            var find_word = db.Words.FirstOrDefault(w => w.Id == Convert.ToInt32(word));

            var find_cat = db.WordCategories.FirstOrDefault(w => w.Id == Convert.ToInt32(category));

            if (find_word != null && find_cat != null)
            {
                //find_word.WordType = find_type;
                //db.Words.Update(find_word);
                //db.SaveChanges();
                return JsonConvert.SerializeObject(new
                {
                    wordId = word,
                    catId = find_cat.Id,
                    catName = find_cat.Name
                });
            }

            return "BAD";
        }

        [HttpGet]
        public IActionResult EditWord(string id)
        {
            var find_word = db.Words.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            return View(find_word);
        }
    }
}