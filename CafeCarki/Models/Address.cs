using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string UserId { get; set; }
        public string AddressStreet { get; set; }
        public string AddressDoor { get; set; }
        public Boolean Visible { get; set; }

        public Restaurant Restaurant { get; set; }
        public Boolean UseGeo { get; set; }
        public string GeoLocationLatitude { get; set; }
        public string GeoLocationLongitude { get; set; }
        public string GeoLocationAccuracy { get; set; }
        public string AddressFromLocation { get; set; }
        public string GeoError { get; set; }
    }
}
