using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.ViewModels
{
    public class AddUserToRolePostViewModel
    {
        public string UserId { get; set; }
        public List<string> RoleIdList { get; set; }
    }
}
