using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string OpenTime { get; set; }
        public string ClosedTime { get; set; }
        public Boolean Broadcast { get; set; }
        public Boolean AdminOkey { get; set; }
        public List<Restaurant> Sube { get; set; }
        public Boolean isSube { get; set; }
        public MenuRestaurant Menu { get; set; }
        public List<Comment> Comment { get; set; }
    }
}
