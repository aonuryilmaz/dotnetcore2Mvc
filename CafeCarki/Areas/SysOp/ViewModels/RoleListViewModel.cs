using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.ViewModels
{
    public class RoleListViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public int NumberOfUsersInRole { get; set; }
    }
}
