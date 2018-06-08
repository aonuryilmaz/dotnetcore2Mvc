using CafeCarki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Controllers
{
    public class MasterController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public MasterController(
            UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager,
             RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string code)
        {
            if (code == "cafecarki")
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "SysOp"
                });
                var user = new ApplicationUser
                {
                    Name = "sercan",
                    Surname = "kal",
                    DateJoined = DateTime.Now,
                    UserName = "sercankal@sercan.com",
                    EmailConfirmed = true,
                    Email = "sercankal@sercan.com"
                };
                var result = await _userManager.CreateAsync(user, "123456");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "SysOp");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    // !!ERROR!!
                    // Kullancı Oluşturulamadı
                }
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
