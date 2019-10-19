using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BirthdayWeb.Models;
using BirthdayWeb.ViewModels;
using BirthdayWeb.Domain.Abstract;

namespace BirthdayWeb.Controllers
{
    [Authorize]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private ILogRepository log;

        public RoleAdminController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMgr, ILogRepository log)
        {
            roleManager = roleMgr;
            userManager = userMgr;
            this.log = log;
        }

        public ViewResult Index() => View(roleManager.Roles.OrderBy(r => r.Name));

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid){
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    log.SaveMessage(new RoleAdminSuccess() { Action = "Create", Message = name });
                    return RedirectToAction("Index");
                }
                else
                {
                    log.SaveMessage(new RoleAdminFailed() { Action = "Create", Message = name });
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            if (role != null) {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    log.SaveMessage(new RoleAdminSuccess() { Action = "Delete", Message = role.Name });
                    return RedirectToAction("Index");
                }
                else
                {
                    log.SaveMessage(new RoleAdminFailed() { Action = "Delete", Message = role.Name });
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                log.SaveMessage(new RoleAdminFailed() { Action = "Delete", Message = "No role found" });
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", roleManager.Roles);
        }
                
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach(var user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel { Role = role, Members = members, NonMembers = nonMembers });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach(string userId in model.IdsToAdd ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if(user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                        {
                            log.SaveMessage(new RoleAdminSuccess() { Action = "Edit", Message = $"Add user {user.UserName} to role {model.RoleName}" });
                        }
                        else
                        {
                            log.SaveMessage(new RoleAdminFailed() { Action = "Edit", Message = $"Add user {user.UserName} to role {model.RoleName}" });
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach(string userId in model.IdsToDelete ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if(user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                        {
                            log.SaveMessage(new RoleAdminSuccess() { Action = "Edit", Message = $"Delete user {user.UserName} from role {model.RoleName}" });
                        }
                        else
                        {
                            log.SaveMessage(new RoleAdminFailed() { Action = "Edit", Message = $"Delete user {user.UserName} from role {model.RoleName}" });
                            AddErrorsFromResult(result);
                        }
                            
                    }
                }                
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors) 
                ModelState.AddModelError("", error.Description);
        }
    }
}
