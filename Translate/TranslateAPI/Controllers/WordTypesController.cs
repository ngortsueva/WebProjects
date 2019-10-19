using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.Domain;
using TranslateAPI.Models;

namespace TranslateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordTypesController : ControllerBase
    {
        private TranslateDb db;

        public WordTypesController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WordType>> Get()
        {
            return db.WordTypes.OrderBy(w => w.Name).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<WordType> Get(string id)
        {
            return db.WordTypes.FirstOrDefault(w => w.Id == Convert.ToInt32(id));
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody]WordType wordType)
        {
            if (ModelState.IsValid)
            {
                var find_verb = db.WordTypes.FirstOrDefault(w => w.Name == wordType.Name);                    

                if (find_verb == null)
                {
                    db.WordTypes.Add(wordType);                       
                    db.SaveChanges();
                    return "OK";
                }                                                    
            }
            return "BAD";
        }
                

        [HttpPut]
        public ActionResult<string> Put([FromBody]WordType wordType)
        {
            if (ModelState.IsValid)
            {
                db.WordTypes.Update(wordType);
                db.SaveChanges();                
                return "OK";
            }
            return "BAD";
        }

        [HttpDelete]
        public ActionResult<string> Delete(string id)
        {
            var find_verb = db.WordTypes.FirstOrDefault(w => w.Id == Convert.ToInt32(id));

            if (find_verb != null)
            {
                db.WordTypes.Remove(find_verb);
                db.SaveChanges();
                return "OK";
            }
            return "BAD";
        }
    }
}