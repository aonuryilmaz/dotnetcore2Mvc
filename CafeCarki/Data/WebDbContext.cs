using CafeCarki.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Data
{
    public class WebDbContext:DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options):base(options) 
        {}
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuRestaurant> MenuRestaurant { get; set; }
        public DbSet<MenuCategory> MenuCategory { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Basket> Basket { get; set; }
    }
}
