using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Boolean isContainer { get; set; }
    }
}
