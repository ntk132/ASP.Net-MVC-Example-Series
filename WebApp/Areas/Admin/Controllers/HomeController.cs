using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Areas.Admin.Models;
using WebApp.Context;

namespace WebApp.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: Admin/Admin
        public ActionResult Index()
        {
            var data = new DBTable();

            // Get 5 latest users registed
            data.Users = db.Users.OrderByDescending(u => u.UserRelease).Take(5).ToList();

            // Get 5 lasted posts released
            data.Posts = db.Posts.OrderByDescending(p => p.PostRelease).Take(5).ToList();

            // Get 5 lasted products released
            data.Products = db.Products.OrderByDescending(p => p.ProductRelease).Take(5).ToList();

            return View(data);
        }
    }
}