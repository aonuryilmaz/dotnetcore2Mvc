using CafeCarki.Areas.SysOp.ViewModels;
using CafeCarki.Data;
using CafeCarki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.Controllers
{
    [Area("SysOp")]
    
    public class UserController:Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationcontext;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext applicationcontext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationcontext = applicationcontext;
        }
        public async Task<IActionResult> Index(string msg, string sortOrder, string searchString, int? page)
        {
            ViewBag.menu = "User";
            ViewBag.Msg = msg;

            List<UserListViewModel> result = new List<UserListViewModel>();
            foreach (var user in _userManager.Users)
            {
                UserListViewModel newUser = new UserListViewModel();
                newUser.Name = user.Name;
                newUser.Surname = user.Surname;
                newUser.DateJoined = user.DateJoined;
                newUser.DateLastLogin = user.DateLastLogin;
                newUser.Email = user.Email;
                newUser.EmailVerify = user.EmailConfirmed;
                newUser.MobilePhone = user.MobilePhone;
                newUser.NumberOfLogins = user.NumberOfLogins;
                newUser.UserId = user.Id;
                newUser.Roles = (await _userManager.GetRolesAsync(user)).ToList();
                newUser.LockoutEnabled = await _userManager.IsLockedOutAsync(user);
                result.Add(newUser);
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DateLastSortParm = sortOrder == "Datelast" ? "datelast_desc" : "Datelast";
            ViewBag.NumberOfLogins = sortOrder == "Logins" ? "logins_desc" : "Logins";
            ViewBag.CurrentFilter = searchString;

            var users = from s in result select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(w => w.Email.ToLower().Contains(searchString.ToLower()));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(o => o.Name);
                    break;
                case "Date":
                    users = users.OrderBy(o => o.DateJoined);
                    break;
                case "date_desc":
                    users = users.OrderByDescending(o => o.DateJoined);
                    break;
                case "Datelast":
                    users = users.OrderBy(o => o.DateLastLogin);
                    break;
                case "datelast_desc":
                    users = users.OrderByDescending(o => o.DateLastLogin);
                    break;
                case "Logins":
                    users = users.OrderBy(o => o.NumberOfLogins);
                    break;
                case "logins_desc":
                    users = users.OrderByDescending(o => o.NumberOfLogins);
                    break;
                default:
                    users = users.OrderBy(o => o.Name);
                    break;
            }

            int pageSize = 4;
            int totalPage = (int)Math.Ceiling((decimal)((double)result.Count / (double)pageSize));
            ViewBag.TotalPage = Enumerable.Range(1, totalPage).Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString(), Selected = (s == page) }).ToList();

            int pageNumber = (page ?? 1);
            ViewBag.Page = pageNumber;
            pageNumber--;
            var data = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            return View(data);
        }
        public async Task<IActionResult> Ban(string id)
        {
            var user = await _applicationcontext.Users.FirstOrDefaultAsync(f => f.Id == id);
            if (user != null)
            {
                var durum = !user.LockoutEnabled;
                await _userManager.SetLockoutEnabledAsync(user, durum);
                if (durum)
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

                _applicationcontext.Entry(user).State = EntityState.Modified;
                await _applicationcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                // !ERROR! !LOG!
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserEditViewModel model = new UserEditViewModel();
            var user = await _applicationcontext.Users.FirstOrDefaultAsync(f => f.Id == id);
            if (user != null)
            {
                model.UserId = user.Id;
                model.Name = user.Name;
                model.Surname = user.Surname;
                model.MobilePhone = user.PhoneNumber;
                model.Email = user.Email;
            }
            else
            {
                // !ERROR! !LOG!
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                var user = _applicationcontext.Users.FirstOrDefault(f => f.Id == model.UserId);
                if (user != null)
                {
                    user.PhoneNumber = model.MobilePhone;
                    user.Email = model.Email;
                    _applicationcontext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _applicationcontext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else

                {
                    // !LOG! !ERROR!
                    msg = "Kullanıcı Bulunamadı.";
                    return RedirectToAction("Index", new { msg = msg });
                }

            }
            return View(model);
        }
    }
}
