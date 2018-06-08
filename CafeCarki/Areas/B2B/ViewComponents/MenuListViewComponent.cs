using CafeCarki.Areas.B2B.ViewModels;
using CafeCarki.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.B2B.ViewComponents
{
    public class MenuListViewComponent:ViewComponent
    {
        private readonly WebDbContext _context;
        public MenuListViewComponent(WebDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int parentId)
        {
            MenuListViewModel result = new MenuListViewModel();
            var backmenu = _context.Menu.FirstOrDefault(f => f.MenuId == parentId);
            if (backmenu != null)
            {
                result.RootMenu = backmenu;
                //ViewBag.backId = backmenu.ParentId;
            }
            result.MenuList= _context.Menu.Where(w => w.ParentId == parentId).ToList();
            return View(result);
        }
    }
}
