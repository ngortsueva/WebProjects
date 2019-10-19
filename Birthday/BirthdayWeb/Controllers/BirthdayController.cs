using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BirthdayWeb.Models;
using BirthdayWeb.Domain;
using BirthdayWeb.Domain.Abstract;

namespace BirthdayWeb.Controllers
{
    [Authorize]
    public class BirthdayController : Controller
    {
        private IPersonRepository repository;        

        public BirthdayController(IPersonRepository repo)
        {
            repository = repo;            
        }

        public IActionResult Index()
        {           
            return View(repository.Persons.Where(p => p.UserName == User.Identity.Name));
        }

        public IActionResult Create(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Person person, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                person.UserName = User.Identity.Name;
                repository.SavePerson(person);
                return Redirect(returnUrl ?? "/Birthday");
            }
            else return View(person);
        }
                
        public IActionResult Edit(string id, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            Person person = repository.Persons.FirstOrDefault(p => p.Id == Convert.ToInt32(id));
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person person, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                repository.SavePerson(person);
                return RedirectToAction(returnUrl ?? "/Birthday");
            }
            else return View(person);            
        }

        public IActionResult Delete(string id)
        {
            Person person = repository.Persons.FirstOrDefault(pf => pf.Id == Convert.ToInt32(id));
            if (person != null)
            {
                repository.DeletePerson(person);                
            }
            return RedirectToAction("Index");
        }

        public IActionResult Calendar()
        {
            var persons = repository.Persons
                .Where(p => p.UserName == User.Identity.Name)
                .OrderBy(p => p.Birthday.Day);

            return View(persons);                                                            
        }
    }
}