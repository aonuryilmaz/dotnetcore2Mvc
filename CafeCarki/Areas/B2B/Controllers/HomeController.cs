using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.B2B.Controllers
{
    [Area("B2B")]
    [Authorize]
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
