using CafeCarki.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.ViewModels
{
    public class AddUserToRoleViewModel
    {
        public ApplicationUser User { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<string> ActiveRoles { get; set; }
    }
}
