using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BirthdayWeb.Models;
using BirthdayWeb.Domain;
using BirthdayWeb.Domain.Abstract;

namespace BirthdayWeb.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private INoteRepository repository;

        public NotesController(INoteRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(string category)
        {
            if (category == "empty")
            {
                return View(repository.Notes.Where(n => n.UserName == User.Identity.Name)
                                        .Where(c => c.Category == null));
            }

            return View(repository.Notes.Where(n => n.UserName == User.Identity.Name)
                                        .Where(c => category == null || c.Category == category));
        }
        
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(UserNote note)
        {
            if(ModelState.IsValid)
            {
                note.UserName = User.Identity.Name;
                note.Date = DateTime.Now;
                repository.Save(note);
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public IActionResult Edit(string id)
        {
            UserNote note = repository.Notes.FirstOrDefault(n => n.Id == Convert.ToInt32(id));
            return View(note);
        }

        [HttpPost]
        public IActionResult Edit(UserNote note)
        {
            if (ModelState.IsValid)
            {
                repository.Save(note);
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public IActionResult Delete(string id)
        {
            UserNote note = repository.Notes.FirstOrDefault(n => n.Id == Convert.ToInt32(id));
            if(note != null)
            {
                repository.Delete(note);
            }            
            return RedirectToAction("Index");
        }
    }
}
