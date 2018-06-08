using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class MenuCategory
    {
        public int MenuCategoryId{ get; set; }
        public string Name { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public MenuRestaurant MenuRestaurant { get; set; }
        public List<MenuCategory> MenuCategories { get; set; }
        public Boolean isChild { get; set; }
    }
}
