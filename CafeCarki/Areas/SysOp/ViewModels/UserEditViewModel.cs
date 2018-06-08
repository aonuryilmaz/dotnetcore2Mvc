using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.SysOp.ViewModels
{
    public class UserEditViewModel
    {
        [HiddenInput]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
    }
}
