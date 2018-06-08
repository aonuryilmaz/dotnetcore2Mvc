using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class Basket
    {
        public int BasketId { get; set; }
        public string UserId { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public decimal TotalPrice { get {
                decimal totalPrice = 0;
                if(this.MenuItems.Count>0 && this.MenuItems != null)
                {
                    foreach (var item in this.MenuItems)
                    {
                        totalPrice += item.Price;
                    }
                }
                return totalPrice;
            } }
        
    }
}
