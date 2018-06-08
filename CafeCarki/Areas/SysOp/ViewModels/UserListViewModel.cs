using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.ViewModels
{
    public class UserListViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Boolean LockoutEnabled { get; set; }
        public bool EmailVerify { get; set; }
        public string MobilePhone { get; set; }
        public int NumberOfLogins { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateJoined { get; set; }
        public DateTime DateLastLogin { get; set; }
        public List<String> Roles { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
    }
}
