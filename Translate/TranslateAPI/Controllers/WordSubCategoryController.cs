using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranslateAPI.Domain;
using TranslateAPI.Models;

namespace TranslateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordSubCategoryController : ControllerBase
    {
        private TranslateDb db;

        public WordSubCategoryController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WordSubCategory>> Get(string category = "")
        {
            if (category != "" && category != "all")
            {
                var cat = db.WordCategories.FirstOrDefault(w => w.Id == Convert.ToInt32(category));
                
                if(cat != null)
                    return db.WordSubCategories
                        .Include(w => w.Category)
                        .Where(w => w.Category.Id == cat.Id)
                        .OrderBy(w => w.Name)
                        .ToList();
            }

            return db.WordSubCategories
                .Include(w => w.Category)
                .OrderBy(w => w.Name).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<WordSubCategory> Get(int id)
        {
           return db.WordSubCategories
                .Include(w => w.Category)
                .FirstOrDefault(w => w.Id == id);           
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody]WordSubCategory wordSubCategory)
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
                return "OK";
            }
            return "BAD";
        }               

        [HttpPut]
        public ActionResult<string> Put([FromBody]WordSubCategory wordSubCategory)
        {
            if (ModelState.IsValid)
            {
                var cat = db.WordCategories.FirstOrDefault(w => w.Id == wordSubCategory.Category.Id);
                wordSubCategory.Category = cat;

                db.WordSubCategories.Update(wordSubCategory);
                db.SaveChanges();
                return "OK";
            }
            return "BAD";
        }

        [HttpDelete]
        public ActionResult<string> Delete(string id)
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
                return "OK";
            }
            return "BAD";
        }
    }
}