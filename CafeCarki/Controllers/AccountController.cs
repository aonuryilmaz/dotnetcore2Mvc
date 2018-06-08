using CafeCarki.Models;
using CafeCarki.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    DateJoined = DateTime.Now,
                    RegisterIp = HttpContext.Connection.RemoteIpAddress.ToString(),
                    RegisterClient=HttpContext.Request.Headers["User-Agent"],
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {

                }
                else
                {

                }
            }
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Code == "InvalidEmail")
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz E-posta adresi.");
                }
                else if (error.Code == "DuplicateUserName")
                {
                    // Username ile email aynı olduğu için bunu kullanmıyoruz
                }
                else if (error.Code == "DuplicateEmail")
                {
                    ModelState.AddModelError(string.Empty, "E-posta adresiniz sistemde zaten kayıtlı. Farklı bir e-posta adresi kullanın ya da üye girişi yapın.");
                }
                else if (error.Code == "InvalidToken")
                {
                    ModelState.AddModelError(string.Empty, "Hatalı güvenlik kodu. Güvenlik kodunu tekrar isyetin.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
    }
}
