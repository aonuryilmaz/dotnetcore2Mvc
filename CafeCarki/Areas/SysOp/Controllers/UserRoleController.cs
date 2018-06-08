using CafeCarki.Areas.SysOp.ViewModels;
using CafeCarki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.Controllers
{
    [Area("SysOp")]
    
    public class UserRoleController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string msg)
        {
            ViewBag.menu = "UserRole";
            ViewBag.Msg = msg;
            List<RoleListViewModel> result = new List<RoleListViewModel>();
            foreach (var item in _roleManager.Roles)
            {
                var listUser = await _userManager.GetUsersInRoleAsync(item.Name);
                RoleListViewModel newRoleList = new RoleListViewModel();
                newRoleList.ConcurrencyStamp = item.ConcurrencyStamp;
                newRoleList.NormalizedRoleName = item.NormalizedName;
                newRoleList.NumberOfUsersInRole = listUser.Count;
                newRoleList.RoleId = item.Id;
                newRoleList.RoleName = item.Name;
                result.Add(newRoleList);
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
            }
            return RedirectToAction("Index", new { msg = msg });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            string msg = "";
            if (!String.IsNullOrEmpty(roleId))
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                    msg = "Role Silindi.";
                }
                else
                {
                    //!ERROR! !LOG!
                    //Role bulunamadı
                    msg = "Role Silinemedi.";
                }
            }
            else
            {
                // !ERROR! id boş gönderilemez
                msg = "Kullanıcı bulunamadı";
            }
            return RedirectToAction("Index", new { msg = msg });
        }
        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
        {
            string msg = "";
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                    msg = user.Email + " kullanıcısından silindi " + role.Name + ".";
                }
                else
                {
                    //!ERROR!
                    //role bulunamadı
                    msg = "Role Bulunamadı.";
                }
            }
            else
            {
                //!ERROR! 
                //Kullanıcı bulunamadı
                msg = "Kullanıcı Bulunamadı";
            }
            return RedirectToAction("Index", "User", new { msg = msg });
        }
        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string userId)
        {
            AddUserToRoleViewModel result = new AddUserToRoleViewModel();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = new List<string>(await _userManager.GetRolesAsync(user));
            if (user != null)
            {
                var roleList = _roleManager.Roles.ToList();
                result.Roles = roleList;
                result.ActiveRoles = roles;
                result.User = user;
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRolePostViewModel model)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    foreach (var item in model.RoleIdList)
                    {
                        IdentityRole role = await _roleManager.FindByIdAsync(item);
                        if (role != null)
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                    }
                }
                else
                {
                    // !ERROR!
                    // Kullanıcı bulunamadı
                    msg = "Kullanıcı Bulunamadı";
                }
                return RedirectToAction("Index", "User", new { msg = msg });
            }
            else
            {
                return RedirectToAction("AddUserToRole", new { userId = model.UserId });
            }
        }
    }
}
