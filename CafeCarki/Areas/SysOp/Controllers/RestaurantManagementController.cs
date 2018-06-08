using CafeCarki.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.Controllers
{
    [Area("SysOp")]
    [Authorize(Roles ="SysOp")]
    public class RestaurantManagementController:Controller
    {
        private readonly WebDbContext _context;
        public RestaurantManagementController(WebDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.menu = "RestaurantManagement";
            var list = _context.Restaurant.Include(i=>i.Sube).ToList();
            return View(list);
        }
        public IActionResult ChangeStatus(int id)
        {
            var restaurant = _context.Restaurant.FirstOrDefault(f => f.RestaurantId == id);
            if (restaurant != null)
            {
                restaurant.AdminOkey = !restaurant.AdminOkey;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
