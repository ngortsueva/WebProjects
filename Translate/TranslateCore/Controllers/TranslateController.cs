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
    public class TranslateController : Controller
    {
        private TranslateDb db;

        public TranslateController(TranslateDb injectDb)
        {
            db = injectDb;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Translate(string wordeng)
        {
            var word = db.Words.Where(w => w.WordEng.Contains(wordeng));

            return Json(JsonConvert.SerializeObject(word));
        }
    }
}