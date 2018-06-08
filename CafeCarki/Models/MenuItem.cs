using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public Boolean Broadcast { get; set; }
        public MenuCategory MenuCategory { get; set; }
    }
}
