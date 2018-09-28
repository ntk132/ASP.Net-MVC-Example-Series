using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Context;

namespace WebApp.Areas.Admin.Models
{
    public class DBTable
    {
        public ICollection<User> Users { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}