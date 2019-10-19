using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BirthdayWeb.Models;
using BirthdayWeb.ViewModels;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Constants;

namespace BirthdayWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        private ILogRepository log;

        public AdminController(UserManager<AppUser> usrMgr,
                                IUserValidator<AppUser> userValid,
                                IPasswordValidator<AppUser> passValid,
                                IPasswordHasher<AppUser> passwordHash,
                                ILogRepository log)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            this.log = log;
        }

        public ViewResult Index() => View(userManager.Users.OrderBy(u => u.UserName));

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            if (ModelState.IsValid) {
                AppUser user = new AppUser {
                    UserName = model.Name,
                    Email = model.Email,
                    Enabled = model.Enabled,
                    Approve = model.Approve
                };
                
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded){
                    log.SaveMessage(new AdminSuccess() { Action = "Create", Message = $"Create account: {user.UserName}" });
                    return RedirectToAction("Index");
                }
                else {
                    log.SaveMessage(new AdminFailed() { Action = "Create", Message = $"Create account: {user.UserName}" });
                    foreach (IdentityError error in result.Errors) {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if(user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    log.SaveMessage(new AdminSuccess() { Action = "Delete", Message = user.UserName });
                    RedirectToAction("Index");
                }
                else
                {
                    log.SaveMessage(new AdminFailed() { Action = "Delete", Message = user.UserName });
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                log.SaveMessage(new AdminFailed() { Action = "Delete", Message = $"User {user.UserName}  not found" });
                ModelState.AddModelError("", "User not found");
            }
            return View("Index", userManager.Users);
        }        

        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null) {
                var userModel = new UserModel() { Id = user.Id, Email = user.Email, Enabled = user.Enabled, Approve = user.Approve };
                return View(userModel);
            }
            else {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModel userModel)
        {
            AppUser user = await userManager.FindByIdAsync(userModel.Id);
            if(user != null)
            {
                user.Email = userModel.Email;

                // Check email
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                    log.SaveMessage(new AdminFailed() { Action = "Edit", Message = $"{user.UserName}: Invalid email" });                    
                }

                // Check password
                IdentityResult validPass = null;

                if (!string.IsNullOrEmpty(userModel.Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, userModel.Id);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, userModel.Id);
                        log.SaveMessage(new AdminSuccess() { Action = "Edit", Message = $"{user.UserName}: Password is OK" });                                                
                    }
                    else
                    {                        
                        AddErrorsFromResult(validPass);
                        log.SaveMessage(new AdminFailed() { Action = "Edit", Message = $"{user.UserName}: Invalid password" });
                    }
                }

                user.Enabled = userModel.Enabled;
                user.Approve = userModel.Approve;

                // Update user account if email and password waw valid
                if ((validEmail.Succeeded && validPass == null)
                    || (validEmail.Succeeded && userModel.Password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        log.SaveMessage(new AdminSuccess() { Action = "Edit", Message = $"Account {user.UserName} was updated" });
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        log.SaveMessage(new AdminFailed() { Action = "Edit", Message = $"Account {user.UserName} was not updated" });
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                log.SaveMessage(new AdminFailed() { Action = "Edit", Message = "User not found" });
                ModelState.AddModelError("", "User not found");
            }
            return View();
        }
        
        public IActionResult ListWaitApprove()
        {
            return View(userManager.Users.Where(u => u.Enabled == false).OrderBy(u => u.UserName));
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}