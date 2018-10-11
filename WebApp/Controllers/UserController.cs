using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Context;
using WebApp.Models;
using PagedList;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: User
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_decs" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_decs" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = from u in db.Users select u;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_decs":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                case "Date":
                    users = users.OrderBy(u => u.UserRelease);
                    break;
                case "date_decs":
                    users = users.OrderByDescending(u => u.UserRelease);
                    break;
                default:
                    users = users.OrderBy(u => u.UserName);
                    break;
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //User user = db.Users.Find(id);
            var user = db.Users.Include(u => u.Posts).Where(p => p.UserID == id.Value)
                .Include(u => u.Products).Where(p => p.UserID == id.Value).Single();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
