using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TranslateAPI.Domain;
using TranslateAPI.Models;

namespace TranslateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private TranslateDb db;

        public DictionaryController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Word>> Get(string filter = "all", int offset = 0, int limit = 0)
        {            
            if(filter != "all")
            {
                Response.Cookies.Append("filter", filter);
                return db.Words.Where(w => w.WordEng.StartsWith(filter)).ToList();
            }
            Response.Cookies.Delete("filter");
            
            return db.Words.ToList();
        }        

        [HttpGet("{id}")]
        public ActionResult<Word> Get(string id)
        {
            return db.Words.FirstOrDefault(w => w.Id == Convert.ToInt32(id));
        }
                
        [HttpPost]
        public ActionResult<string> Post([FromBody]Word word)
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
                    return "OK";
                }
            }
            return "BAD";           
        }
        
        [HttpPut]
        public ActionResult<string> Put([FromBody]Word word)
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
                return "OK";
            }
            return "BAD";
        }

        [HttpDelete]
        public ActionResult<string> Delete(string id)
        {
            var find_word = db.Words.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if(find_word != null)
            {
                db.Words.Remove(find_word);
                db.SaveChanges();
                return "OK";
            }
            return "BAD";
        }
    }
}