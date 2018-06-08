using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeCarki.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
