using CafeCarki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.B2B.ViewModels
{
    public class MenuListViewModel
    {
        public Menu RootMenu { get; set; }
        public List<Menu> MenuList { get; set; }
    }
}
