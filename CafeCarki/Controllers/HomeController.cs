using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CafeCarki.Models;
using Microsoft.AspNetCore.Identity;
using CafeCarki.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CafeCarki.ViewModels;
using CafeCarki.Services;
using Newtonsoft.Json;

namespace CafeCarki.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebDbContext _context;
        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext applicationDbContext,
            RoleManager<IdentityRole> roleManager,
            WebDbContext context
            )
        {
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string src="desktop")
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Panel");
            }
            if (src == "mobile")
            {
                return View("Mobile");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model,string MobileLogin=null)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _applicationDbContext.Users.FirstOrDefaultAsync(f => f.Email == model.Email);
                    if (user != null)
                    {
                        user.DateLastLogin = DateTime.Now;
                        user.NumberOfLogins++;
                        _applicationDbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await _applicationDbContext.SaveChangesAsync();
                        if (MobileLogin != null)
                        {
                            return RedirectToAction("Index", "Home", new { area = "Mobile" });
                        }
                        return RedirectToAction("Index","Home",new { area="B2B" });
                    }
                    else
                    {
                        //!!ERROR!!
                        //Kullanıcı bulunamadı
                        return RedirectToAction("Index");
                    }

                }
                if (result.IsLockedOut)
                {

                    ModelState.AddModelError("", "Hesabınız Engellenmiştir.");
                    return View(model);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }
        public IActionResult Register(string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model,string MobileRegister=null, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name=model.Name,
                    Surname = model.Surname,
                    DateJoined = DateTime.Now,
                    PhoneNumber = model.Phone,
                    RegisterIp = HttpContext.Connection.RemoteIpAddress.ToString(),
                    RegisterClient = HttpContext.Request.Headers["User-Agent"],
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Basket newBasket = new Basket();
                    newBasket.UserId = user.Id;
                    _context.Basket.Add(newBasket);
                    _context.SaveChanges();
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackurl = Url.Action("ConfirmEmail", "Home", new { userId = user.Id, code = code });
                    //Email eklenecek
                    await _signInManager.SignInAsync(user, isPersistent : false);
                    if (MobileRegister != null)
                    {
                        if(!(await _roleManager.RoleExistsAsync("Mobile")))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Mobile"));
                        }
                        await _userManager.AddToRoleAsync(user, "Mobile");
                        return RedirectToAction("Index", "Home", new { area="Mobile" });
                    }
                    TempData["StatusMessage"] = JsonConvert.SerializeObject(new StatusMessageViewModel
                    {
                        Type = StatusMessageType.Success,
                        Category = StatusMessageCategory.User,
                        Date = DateTime.Now,
                        Title = "Üye kaydınız oluşturuldu",
                        Message = "Lütfen e-posta adresinizi onaylayın."
                    });
                    return RedirectToLocal(returnUrl);
                }
            }
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Panel", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
