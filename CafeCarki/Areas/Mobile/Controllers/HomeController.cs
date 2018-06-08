using CafeCarki.Data;
using CafeCarki.Models;
using CafeCarki.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.Mobile.Controllers
{
    [Area("Mobile")]
    public class HomeController : Controller
    {
        private readonly WebDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(WebDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var list = _context.Restaurant.Include(i => i.Sube).ThenInclude(t => t.Comment).Include(i => i.Comment).Where(w => w.AdminOkey).ToList();
            return View(list);
        }
        public IActionResult Detail(int id)
        {
            var restaurant = _context.Restaurant
                .Include(i => i.Menu)
                .ThenInclude(t => t.MenuCategories)
                .ThenInclude(t => t.MenuItems)
                .Include(i => i.Menu)
                .ThenInclude(t => t.MenuCategories)
                .ThenInclude(t => t.MenuCategories)
                .ThenInclude(t => t.MenuItems)
                .Include(i => i.Comment).FirstOrDefault(f => f.RestaurantId == id);
            if (restaurant != null)
            {
                return View(restaurant);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(string Yorum, int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var restaurant = _context.Restaurant.FirstOrDefault(f => f.RestaurantId == id);
            if (restaurant != null)
            {
                if (!String.IsNullOrEmpty(Yorum))
                {
                    Comment newComment = new Comment();
                    newComment.Restaurant = restaurant;
                    newComment.Text = Yorum;
                    newComment.UserId = user.Id;
                    newComment.UserName = user.UserName;
                    _context.Comment.Add(newComment);
                    _context.SaveChanges();
                }
            }
            TempData["StatusMessage"] = JsonConvert.SerializeObject(new StatusMessageViewModel
            {
                Type = StatusMessageType.Success,
                Category = StatusMessageCategory.User,
                Date = DateTime.Now,
                Message = "Yorumunuz başarıyla eklendi."
            });
            return RedirectToAction("Detail", new { id = id });
        }
        public async Task<IActionResult> AddBasket(int SubeId, int MenuItemId)
        {
            var user = await _userManager.GetUserAsync(User);
            var basket = _context.Basket.Include(i=>i.MenuItems).FirstOrDefault(f => f.UserId == user.Id);
            var menuItem = _context.MenuItem.FirstOrDefault(f => f.MenuItemId == MenuItemId);
            if (basket != null)
            {
                if (basket.MenuItems == null)
                {
                    basket.MenuItems = new List<MenuItem>();
                }
                basket.MenuItems.Add(menuItem);
                _context.SaveChanges();
            }
            else
            {
                Basket newBasket = new Basket();
                newBasket.MenuItems = new List<MenuItem>();
                newBasket.UserId = user.Id;
                newBasket.MenuItems.Add(menuItem);
                _context.Basket.Add(newBasket);
                _context.SaveChanges();
            }
            TempData["StatusMessage"] = JsonConvert.SerializeObject(new StatusMessageViewModel {
                Type = StatusMessageType.Success,
                Category = StatusMessageCategory.User,
                Date = DateTime.Now,
                Message = "Ürün sepete başarıyla eklendi"
            });
            return RedirectToAction("Detail", new { id = SubeId });
        }
        public async Task<IActionResult> RemoveBasket(int SubeId, int MenuItemId)
        {
            var user = await _userManager.GetUserAsync(User);
            var basket = _context.Basket.Include(i => i.MenuItems).FirstOrDefault(f => f.UserId == user.Id);
            var menuItem = basket.MenuItems.FirstOrDefault(f => f.MenuItemId ==MenuItemId);
            basket.MenuItems.Remove(menuItem);
            _context.SaveChanges();
            TempData["StatusMessage"] = JsonConvert.SerializeObject(new StatusMessageViewModel
            {
                Type = StatusMessageType.Success,
                Category = StatusMessageCategory.User,
                Date = DateTime.Now,
                Message = "Ürün sepetten başarıyla çıkartıldı."
            });
            return RedirectToAction("Index");
        }
    }
}
