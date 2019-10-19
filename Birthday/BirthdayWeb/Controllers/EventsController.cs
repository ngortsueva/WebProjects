using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Domain;
using BirthdayWeb.Models;

namespace BirthdayWeb.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private IEventRepository repository;    
        
        public EventsController(IEventRepository repo) { repository = repo; }

        public IActionResult Index()
        {
            return View(repository.Events.Where(t => t.UserName == User.Identity.Name)
                                        .OrderBy(t => t.BeginTime));
        }

        public IActionResult Filter(string filter, string value)
        {
            return View();
        }

        public IActionResult Create()
        {
            UserEvent userEvent = new UserEvent
            {
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),                
                Notify = true,
                RepeatNotify = false,
                RepeatCount = 1,
                UserName = User.Identity.Name
            };
            return View(userEvent);
        }

        [HttpPost]
        public IActionResult Create(UserEvent userEvent)
        {
            if (ModelState.IsValid)
            {
                userEvent.CreateTime = DateTime.Now;
                userEvent.ModifyTime = DateTime.Now;
                userEvent.UserName = User.Identity.Name;
                repository.SaveEvent(userEvent);
                return RedirectToAction("Index");
            }
            return View(userEvent);
        }

        public IActionResult Edit(string id)
        {
            UserEvent userEvent = repository.Events.FirstOrDefault(t => t.Id == Convert.ToInt32(id));
            return View(userEvent);
        }

        [HttpPost]
        public IActionResult Edit(UserEvent userEvent)
        {
            if (ModelState.IsValid)
            {
                userEvent.ModifyTime = DateTime.Now;
                userEvent.UserName = User.Identity.Name;
                repository.SaveEvent(userEvent);
                return RedirectToAction("Index");
            }
            return View(userEvent);
        }

        public IActionResult Delete(string id)
        {
            UserEvent userEvent = repository.Events.FirstOrDefault(t => t.Id == Convert.ToInt32(id));

            if (userEvent != null)
            {
                repository.DeleteEvent(userEvent);
            }
            return RedirectToAction("Index");
        }
    }
}