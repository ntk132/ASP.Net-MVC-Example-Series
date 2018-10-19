using PagedList;
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

namespace WebApp.Controllers
{
    public class PostController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Post
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

            var posts = from p in db.Posts.Include(p => p.PostMetas) select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.PostTitle.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_decs":
                    posts = posts.OrderByDescending(u => u.PostTitle);
                    break;
                case "Date":
                    posts = posts.OrderBy(u => u.PostModified);
                    break;
                case "date_decs":
                    posts = posts.OrderByDescending(u => u.PostModified);
                    break;
                default:
                    posts = posts.OrderBy(u => u.PostTitle);
                    break;
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);

            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts
                .Include(p => p.User).Where(u => u.User.UserID == u.UserID)
                .Include(p => p.Comments).Where(c => c.PostID == id.Value)
                .Include(p => p.PostMetas).Where(m => m.PostID == id.Value).Single();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
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
