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
    public class WordCategoryController : ControllerBase
    {
        private TranslateDb db;

        public WordCategoryController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WordCategory>> Get(string filter = "all")
        {
            if (filter != "all")
            {
                return db.WordCategories
                    .Where(w => w.Name.StartsWith(filter))
                    .OrderBy(w => w.Name)
                    .ToList();
            }
            return db.WordCategories.OrderBy(w => w.Name).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<WordCategory> Get(int id)
        {
            return db.WordCategories.FirstOrDefault(w => w.Id == id);
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody]WordCategory wordCategory)
        {
            if (ModelState.IsValid)
            {                
                var find_verb = db.WordCategories.FirstOrDefault(w => w.Name == wordCategory.Name);                    

                if (find_verb == null)
                {                    
                    db.WordCategories.Add(wordCategory);                        
                    db.SaveChanges();
                }                    
                return "OK";                
            }
            return "BAD";
        }

        [HttpPut]
        public ActionResult<string> Put([FromBody]WordCategory wordCategory)
        {
            if (ModelState.IsValid)
            {
                db.WordCategories.Update(wordCategory);
                db.SaveChanges();                
                return "OK";
            }
            return "BAD";
        }

        [HttpDelete]
        public ActionResult<string> Delete(string id)
        {
            var find_word = db.WordCategories.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_word != null)
            {
                db.WordCategories.Remove(find_word);
                db.SaveChanges();
                return "OK";
            }
            return "BAD";
        }
    }
}