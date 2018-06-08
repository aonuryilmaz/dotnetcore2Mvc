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
    [Authorize]
    [Area("B2B")]
    public class RestaurantController:Controller
    {
        private readonly WebDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public RestaurantController(WebDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.menu = "Restaurant";
            var user = await _userManager.GetUserAsync(User);
            return View(await _context.Restaurant.Include(i=>i.Menu).Include(i=>i.Address).Include(i=>i.Sube).ThenInclude(t=>t.Address).Include(i=>i.Sube).ThenInclude(i=>i.Menu).Where(w=>w.isSube==false && w.UserId==user.Id).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!String.IsNullOrEmpty(name))
            {
                var result = _context.Restaurant.FirstOrDefault(f => f.Name.ToLower() == name.ToLower());
                if (result == null)
                {
                    Restaurant newRestaurant = new Restaurant
                    {
                        Name = name,
                        UserId=user.Id
                    };
                    _context.Restaurant.Add(newRestaurant);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult CreateSube(int id)
        {
            ViewBag.menu = "Restaurant";
            ViewBag.restaurantId = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSube(string name,int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var restaurant = _context.Restaurant.FirstOrDefault(f => f.RestaurantId == id);
            if (!String.IsNullOrEmpty(name))
            {
                if (restaurant.Sube == null)
                {
                    restaurant.Sube = new List<Restaurant>();
                }
                Restaurant newRestaurant = new Restaurant();
                newRestaurant.Name = name;
                newRestaurant.isSube = true;
                newRestaurant.UserId = user.Id;
                restaurant.Sube.Add(newRestaurant);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewBag.menu = "Restaurant";
            var restaurant = _context.Restaurant.Include(i=>i.Address).FirstOrDefault(f => f.RestaurantId == id);
            return View(restaurant);
        }
        [HttpPost]
        public IActionResult Edit(Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var result = _context.Restaurant.FirstOrDefault(f => f.RestaurantId == model.RestaurantId);
                result.Name = model.Name;
                result.OpenTime = model.OpenTime;
                result.ClosedTime = model.ClosedTime;
                result.Broadcast = model.Broadcast;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
