using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BirthdayWeb.Domain.Abstract;

namespace BirthdayWeb.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private INoteRepository repository;

        public CategoryList(INoteRepository repo) { repository = repo; }

        public IViewComponentResult Invoke()
        {
            return View(repository.Notes
                    .Where(p => p.UserName == User.Identity.Name && p.Category != null)
                    .Select(n => n.Category)                           
                    .Distinct()
                    .OrderBy(n => n)
                );
        }
    }
}
