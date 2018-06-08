using CafeCarki.Data;
using CafeCarki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.B2B.Controllers
{
    [Authorize]
    [Area("B2B")]
    public class MenuRecursiveController:Controller
    {
        private readonly WebDbContext _context;
        public MenuRecursiveController(WebDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id=0)
        {
            ViewBag.menu = "Menu";
            ViewBag.ParentId = id;
            return View();
        }
        public IActionResult Create(int ParentId)
        {
            ViewBag.menu = "Menu";
            Menu newMenu = new Menu();
            newMenu.ParentId = ParentId;
            return View(newMenu);
        }
        [HttpPost]
        public IActionResult Create(Menu model)
        {
            Menu newMenu = new Menu();
            newMenu.Title = model.Title;
            newMenu.Price = model.Price;
            newMenu.ParentId = model.ParentId;
            newMenu.isContainer = model.isContainer;
            _context.Menu.Add(newMenu);
            _context.SaveChanges();
            return RedirectToAction("Index", new { id=model.ParentId });
        }
    }
}
