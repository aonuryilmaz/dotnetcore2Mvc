using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class MenuRestaurant
    {
        public int MenuRestaurantId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<MenuCategory> MenuCategories { get; set; }
    }
}
