using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Areas.B2B.ViewModels
{
    public class AddressCreateViewModel
    {
        [HiddenInput]
        public string UserId { get; set; }
        public string AddressDoor { get; set; }
        public string AddressStreet { get; set; }
        public string AddressName { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
    }
}
