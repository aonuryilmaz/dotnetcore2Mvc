using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CafeCarki.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int NumberOfLogins { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateLastLogin { get; set; }
        public string RegisterIp { get; set; }
        public string RegisterClient { get; set; }
        public string MobilePhone { get; set; }
    }
}
