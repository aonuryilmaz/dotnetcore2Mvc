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

namespace CafeCarki.Areas.B2B.Controllers
{
    [Area("B2B")]
    [Authorize]
    public class MenuController : Controller
    {
        private readonly WebDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MenuController(WebDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var list = _context.MenuRestaurant.Where(w => w.UserId == user.Id).ToList();
            return View(list);
        }
        public async Task<IActionResult> CreateMenuRestaurant(int id = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.MenuList = _context.MenuRestaurant.Where(w => w.UserId == user.Id).Select(s=>new SelectListItem { Value=s.MenuRestaurantId.ToString(),Text=s.Name }).ToList();
            if (id > 0)
            {
                MenuRestaurant newMenu = new MenuRestaurant();
                newMenu.UserId = user.Id;
                ViewBag.RestaurantId = id;
                return View(newMenu);
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateMenuRestaurant(MenuRestaurant model, int RestaurantId)
        {
            var restaurant = _context.Restaurant.FirstOrDefault(f => f.RestaurantId == RestaurantId);
            MenuRestaurant newRestaurant = new MenuRestaurant();
            newRestaurant.Name = model.Name;
            newRestaurant.UserId = model.UserId;
            _context.MenuRestaurant.Add(newRestaurant);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddMenuRestaurant(int Menu,int RestaurantId)
        {
            var restaurant= _context.Restaurant.FirstOrDefault(f => f.RestaurantId == RestaurantId);
            var MenuRestaurant = _context.MenuRestaurant.FirstOrDefault(f => f.MenuRestaurantId == Menu);
            restaurant.Menu = MenuRestaurant;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult EditMenuRestaurant(int MenuRestaurantId)
        {
            var menuRestaurant = _context.MenuRestaurant.Include(i => i.MenuCategories).FirstOrDefault(f => f.MenuRestaurantId == MenuRestaurantId);
            return View(menuRestaurant);
        }
        [HttpPost]
        public IActionResult EditMenuRestaurant(MenuRestaurant model)
        {
            var menuRestaurant = _context.MenuRestaurant.FirstOrDefault(f => f.MenuRestaurantId == model.MenuRestaurantId);
            menuRestaurant.Name = model.Name;
            _context.SaveChanges();
            return View();
        }
        public IActionResult CreateMenuCategory(int MenuRestaurantId, int categoryId)
        {
            ViewBag.MenuRestaurantId = MenuRestaurantId;
            ViewBag.ReturnCatId = categoryId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateMenuCategory(MenuCategory model, int MenuRestaurantId, int retcatId)
        {
            var menuRestaurant = _context.MenuRestaurant.FirstOrDefault(f => f.MenuRestaurantId == MenuRestaurantId);
            if (retcatId > 0)
            {
                var menuCategory = _context.MenuCategory.Include(i=>i.MenuCategories).FirstOrDefault(f => f.MenuCategoryId == retcatId);
                if (menuCategory.MenuCategories == null)
                {
                    menuCategory.MenuCategories = new List<MenuCategory>();
                }
                MenuCategory newCategoryforCat = new MenuCategory();
                newCategoryforCat.Name = model.Name;
                newCategoryforCat.MenuRestaurant = menuRestaurant;
                newCategoryforCat.isChild = true;
                menuCategory.MenuCategories.Add(newCategoryforCat);
                _context.SaveChanges();
                return RedirectToAction("EditMenuCategory", new { MenuRestaurantId = MenuRestaurantId, categoryId = retcatId, });
            }
            MenuCategory newCategory = new MenuCategory();
            newCategory.Name = model.Name;
            newCategory.MenuRestaurant = menuRestaurant;
            if (menuRestaurant.MenuCategories == null)
            {
                menuRestaurant.MenuCategories = new List<MenuCategory>();
            }
            menuRestaurant.MenuCategories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("EditMenuRestaurant", new { MenuRestaurantId = MenuRestaurantId });
        }
        public IActionResult EditMenuCategory(int MenuRestaurantId, int categoryId, int retcatId)
        {
            var menuCategory = _context.MenuCategory.Include(i => i.MenuItems).Include(i => i.MenuCategories).FirstOrDefault(f => f.MenuCategoryId == categoryId);
            ViewBag.MenuRestaurantId = MenuRestaurantId;
            ViewBag.ReturnCatId = retcatId;
            return View(menuCategory);
        }
        [HttpPost]
        public IActionResult EditMenuCategory(MenuCategory model,int MenuRestaurantId,int retcatId)
        {
            var menuCategory = _context.MenuCategory.FirstOrDefault(f => f.MenuCategoryId == model.MenuCategoryId);
            menuCategory.Name = model.Name;
            _context.SaveChanges();
            if (retcatId > 0)
            {
                return RedirectToAction("EditMenuCategory", new { MenuRestaurantId = MenuRestaurantId, categoryId = retcatId });
            }
            return RedirectToAction("EditMenuRestaurant", new { MenuRestaurantId = MenuRestaurantId });
        }
        public IActionResult CreateMenuItem(int categoryId, int MenuRestaurantId)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.MenuRestaurantId = MenuRestaurantId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateMenuItem(MenuItem model,int retcatId,int MenuRestaurantId)
        {
            var MenuCategory = _context.MenuCategory.FirstOrDefault(f => f.MenuCategoryId == retcatId);
            MenuItem newItem = new MenuItem();
            newItem.Name = model.Name;
            newItem.Price = model.Price;
            newItem.MenuCategory = MenuCategory;
            _context.MenuItem.Add(newItem);
            _context.SaveChanges();
            return RedirectToAction("EditMenuCategory",new {categoryId=retcatId, MenuRestaurantId= MenuRestaurantId });
        }
        public IActionResult EditMenuItem(int MenuItemId,int categoryId, int MenuRestaurantId)
        {
            var menuItem = _context.MenuItem.FirstOrDefault(f => f.MenuItemId == MenuItemId);
            ViewBag.CategoryId = categoryId;
            ViewBag.MenuRestaurantId = MenuRestaurantId;
            return View(menuItem);
        }
        [HttpPost]
        public IActionResult EditMenuItem(MenuItem model,int MenuRestaurantId, int categoryId)
        {
            var menuItem = _context.MenuItem.FirstOrDefault(f => f.MenuItemId == model.MenuItemId);
            menuItem.Name = model.Name;
            menuItem.Price = model.Price;
            _context.SaveChanges();
            return RedirectToAction("EditMenuCategory", new { MenuRestaurantId = MenuRestaurantId, categoryId = categoryId });
        }
        public IActionResult ChangeVisible(int id)
        {
            var menuItem = _context.MenuItem.FirstOrDefault(f => f.MenuItemId == id);
            if (menuItem != null)
            {
                menuItem.Broadcast =!menuItem.Broadcast;
                _context.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
    }
}
