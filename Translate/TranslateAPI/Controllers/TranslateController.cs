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
    public class TranslateController : ControllerBase
    {
        private TranslateDb db;

        public TranslateController(TranslateDb injectDb)
        {
            db = injectDb;
        }        

        [HttpGet("{wordeng}")]
        public ActionResult<IEnumerable<Word>> Translate(string wordeng)
        {
            return db.Words.Where(w => w.WordEng.Contains(wordeng)).ToList();
        }
    }
}