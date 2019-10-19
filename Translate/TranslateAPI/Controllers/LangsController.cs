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
    public class LangsController : ControllerBase
    {
        private TranslateDb db;

        public LangsController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        [HttpGet]
        public IEnumerable<Language> Get()
        {
            return db.Languages.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Language> Get(string id)
        {
            return db.Languages.FirstOrDefault(w => w.Id == Convert.ToInt32(id));
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody]Language language)
        {
            if(language != null)
            {
                var lang = db.Languages.FirstOrDefault(l => l.Name == language.Name);

                if(lang == null)
                {
                    db.Languages.Add(language);
                    db.SaveChanges();
                    return "OK";
                }
            }
            return "BAD";
        }

        [HttpPut]
        public ActionResult<string> Put([FromBody]Language language)
        {
            if (ModelState.IsValid)
            {
                db.Languages.Update(language);
                db.SaveChanges();
                return "OK";
            }
            return "BAD";
        }

        [HttpDelete]
        public ActionResult<string> Delete(string id)
        {
            var lang = db.Languages.FirstOrDefault(l => l.Id == Convert.ToInt32(id));

            if(lang != null)
            {
                db.Languages.Remove(lang);
                db.SaveChanges();
                return "OK";
            }
            return "BAD";
        }
    }
}