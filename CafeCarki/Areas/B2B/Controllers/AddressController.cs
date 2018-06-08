using CafeCarki.Areas.B2B.ViewModels;
using CafeCarki.Data;
using CafeCarki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.B2B.Controllers
{
    [Authorize]
    [Area("B2B")]
    public class AddressController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WebDbContext _context;
        private readonly ApplicationDbContext _applicationDbContext;
        public AddressController(WebDbContext context,ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public async Task <IActionResult> Index()
        {
            ViewBag.menu = "Address";
            var user=await _userManager.GetUserAsync(HttpContext.User);
            var adres = _context.Address.Include(i=>i.Restaurant).Where(w => w.UserId == user.Id && w.Visible == true).Select(s => new AddressListViewModel
            {
                UserId = s.UserId,
                AddressDoor = s.AddressDoor,
                AddressId = s.AddressId,
                AddressName = s.AddressName,
                AddressStreet = s.AddressStreet,
                Restaurant=s.Restaurant
            }).ToList();
            return View(adres);
        }
        public async Task<IActionResult> Create(int restaurantId)
        {
            ViewBag.menu = "Address";
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //AddressCreateViewModel model = new AddressCreateViewModel();
            //if (user != null)
            //{
            //    model.UserId = user.Id;
            //}
            ViewBag.restaurantId = restaurantId;
            ViewBag.userId = user.Id;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Address model,int restaurantId)
        {
            if (ModelState.IsValid)
            {
                var restaurant = _context.Restaurant.FirstOrDefault(f => f.RestaurantId == restaurantId);
                Address newAddress = new Address();
                newAddress.AddressDoor = model.AddressDoor;
                newAddress.AddressName = model.AddressName;
                newAddress.AddressStreet = model.AddressStreet;
                newAddress.UserId = model.UserId;
                newAddress.Visible = true;
                newAddress.Restaurant = restaurant;
                _context.Address.Add(newAddress);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.menu = "Address";
            var address = _context.Address.FirstOrDefault(f => f.AddressId == id);
            if (address != null)
            {
                return View(address);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Address model)
        {
            if (ModelState.IsValid)
            {
                var adress = _context.Address.FirstOrDefault(f => f.AddressId == model.AddressId);
                adress.AddressDoor = model.AddressDoor;
                adress.AddressName = model.AddressName;
                adress.AddressStreet = model.AddressStreet;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            _context.Address.Remove(_context.Address.SingleOrDefault(s => s.AddressId == id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
