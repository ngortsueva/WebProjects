using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BirthdayWeb.Models;
using BirthdayWeb.ViewModels;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Constants;

namespace BirthdayWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private ILogRepository log;
        private IRequestRepository request;

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr, ILogRepository log, IRequestRepository req)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            this.log = log;
            request = req;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {            
            ViewBag.returnUrl = returnUrl;
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {            
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if(user != null)
                {
                    if (user.Enabled && user.Approve)
                    {
                        await signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(user, details.Password, false, false);

                        if (result.Succeeded) {
                            log.SaveMessage(new LoginSuccess() { Message  = user.UserName });
                            return Redirect(returnUrl ?? "/Birthday");
                        }                        
                    }
                    else {
                        if(!user.Approve) log.SaveMessage(new LoginFailedUserWaitApprove() { Message = user.UserName } );

                        if (user.Approve && !user.Enabled) log.SaveMessage(new LoginFailedUserLocked() { Message = user.UserName });

                        ModelState.AddModelError(nameof(LoginModel.Email), $"User {user.UserName} is Locked");
                        return View(details);
                    }
                }
                log.SaveMessage(new LoginFailed() { Message = details.Email });

                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            log.SaveMessage(new Logout());
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

        [AllowAnonymous]
        public IActionResult CreateAccount() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount(UserModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    Enabled = true,
                    Approve = false
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    ViewBag.Created = true;
                    log.SaveMessage(new LoginCreateUserSuccess() { Message = $"Create account: {user.UserName}"});

                    request.Create(new RequestMessage() { Date = DateTime.Now,
                                                          ObjectId = user.Id,
                                                          ObjectType = ObjectType.User,
                                                          Message = $"Create account ID: {user.Id}, USERNAME: {user.UserName} - Need Approve." });

                    log.SaveMessage(new RequestCreateSuccess() { Source = "Account", Message = $"Create request for new account: {user.UserName}" });

                    return View(model);
                }
                else
                {
                    log.SaveMessage(new LoginCreateUserFailed());
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}