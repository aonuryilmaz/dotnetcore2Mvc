using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Services
{
    public enum StatusMessageType
    {
        Info,
        Warning,
        Danger,
        Success
    }
    public enum StatusMessageCategory
    {
        User,
        Sysyop
    }
    public class StatusMessageViewModel
    {      
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public StatusMessageType Type { get; set; }
        public StatusMessageCategory Category { get; set; }
    }
}
