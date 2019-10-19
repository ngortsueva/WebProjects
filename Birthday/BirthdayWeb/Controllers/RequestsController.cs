using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;
using BirthdayWeb.ViewModels;
using BirthdayWeb.Constants;

namespace BirthdayWeb.Controllers
{
    public class RequestsController : Controller
    {
        private IRequestRepository repository;
        private ILogRepository log;
        private UserManager<AppUser> userManager;

        public RequestsController(IRequestRepository repo, ILogRepository log, UserManager<AppUser> usrMgr)
        {
            repository = repo;
            this.log = log;
            userManager = usrMgr;            
        }

        public IActionResult Index()
        {
            return View(repository.Requests);
        }

        public IActionResult Clear()
        {
            repository.Clear();

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Approve(string id, string objecttype)
        {
            RequestMessage requestMessage = repository.Requests.FirstOrDefault(m => m.Id == Convert.ToInt32(id));

            if (requestMessage != null)
            {
                switch (objecttype.ToLower())
                {
                    case ObjectType.User:
                        AppUser user = await userManager.FindByIdAsync(requestMessage.ObjectId);

                        user.Enabled = true;
                        user.Approve = true;
                       
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            log.SaveMessage(new RequestApproveSuccess() { Message = $"Account {user.UserName} was approved" });

                            repository.Delete(requestMessage);                            
                        }
                        else
                        {
                            log.SaveMessage(new RequestApproveFailed() { Message = $"Account {user.UserName} ERROR: Failed approve" });
                        }
                        break;
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            RequestMessage message = repository.Requests.FirstOrDefault(m => m.Id == Convert.ToInt32(id));

            if(message != null)
            {
                repository.Delete(message);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewRequest(string id, string objecttype)
        {
            RequestMessage message = repository.Requests.FirstOrDefault(m => m.Id == Convert.ToInt32(id));
            
            if(message != null)
            {
                switch (objecttype.ToLower())
                {
                    case ObjectType.User:
                        AppUser user = await userManager.FindByIdAsync(message.ObjectId);
                        var requestForUser = new RequestUserViewModel
                        {
                            requestMessage = message,
                            UserName = user.UserName,
                            UserEmail = user.Email
                        };
                        return View("ViewRequestForUser", requestForUser);
                }                
            }
            return View();
        }
    }
}