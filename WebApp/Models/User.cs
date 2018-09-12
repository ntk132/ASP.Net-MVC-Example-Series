using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public DateTime UserRelease { get; set; }
        public string UserBio { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Links { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}