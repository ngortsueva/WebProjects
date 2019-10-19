using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranslateCore.Domain;
using TranslateCore.Models;

namespace TranslateCore.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private TranslateDb db;

        public CategoryList(TranslateDb injectDb) { db = injectDb; }

        public IViewComponentResult Invoke()
        {
            return View(db.WordCategories.OrderBy(w => w.Name));
        }
    }
}
