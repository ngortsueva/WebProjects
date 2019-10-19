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
    public class TasksController : Controller
    {
        private ITaskRepository repository;

        public TasksController(ITaskRepository repo) { repository = repo; }

        public IActionResult Index()
        {
            return View(repository.Tasks.Where(t => t.UserName == User.Identity.Name)
                                        .OrderBy(t => t.BeginTime));
        }

        public IActionResult Filter(string filter, string value)
        {
            return View();
        }

        public IActionResult Create()
        {
            UserTask task = new UserTask
            {
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),                 
                Notify = true,
                RepeatNotify = false,
                RelevanceValue = 1,
                Color = 1,
                Flag = 1,
                UserName = User.Identity.Name
            };
            return View(task);
        }

        [HttpPost]
        public IActionResult Create(UserTask task)
        {
            if (ModelState.IsValid)
            {
                task.CreateTime = DateTime.Now;
                task.ModifyTime = DateTime.Now;                
                task.UserName = User.Identity.Name;
                repository.SaveTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public IActionResult Edit(string id)
        {
            UserTask task = repository.Tasks.FirstOrDefault(t => t.Id == Convert.ToInt32(id));
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(UserTask task)
        {
            if (ModelState.IsValid)
            {
                task.ModifyTime = DateTime.Now;
                task.UserName = User.Identity.Name;
                repository.SaveTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public IActionResult Delete(string id)
        {
            UserTask task = repository.Tasks.FirstOrDefault(t => t.Id == Convert.ToInt32(id));

            if (task != null)
            {
                repository.DeleteTask(task);
            }
            return RedirectToAction("Index");
        }
    }
}