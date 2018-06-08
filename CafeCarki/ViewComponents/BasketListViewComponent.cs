using CafeCarki.Data;
using CafeCarki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.Mobile.ViewComponents
{
    public class BasketListViewComponent:ViewComponent
    {
        private readonly WebDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public BasketListViewComponent(WebDbContext context,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var basket = _context.Basket.Include(i => i.MenuItems).FirstOrDefault(f => f.UserId == user.Id);
                return View(basket);
            }            
            return View();
        }
    }
}
