using CafeCarki.Data;
using CafeCarki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.B2B.ViewComponents
{
    public class MenuRenderViewComponent:ViewComponent
    {
        private readonly WebDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MenuRenderViewComponent(WebDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var list = await _context.MenuRestaurant
                .Include(i=>i.MenuCategories)
                .ThenInclude(t=>t.MenuItems)
                .Include(i=>i.MenuCategories)
                .ThenInclude(t=>t.MenuCategories)
                .ThenInclude(t=>t.MenuItems)
                .Where(w => w.UserId == user.Id).ToListAsync();
            return View(list);
        }
    }
}
