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

            var posts = from p in db.Posts select p;

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
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostName,PostTitle,PostContent,PostFormat,PostStatus,CommentStatus,CommentCount,UserID")] Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    post.PostRelease = DateTime.Now;
                    post.PostModified = DateTime.Now;

                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /*de*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", post.UserID);
            return View(post);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", post.UserID);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var postToUpdate = db.Posts.Find(id);

            if (TryUpdateModel(postToUpdate, "", new string[] { "PostName", "PostTitle", "PostContent", "PostFormat", "PostStatus", "CommentStatus", "CommentCount", "UserID" }))
            {
                postToUpdate.PostModified = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", postToUpdate.UserID);
            return View(postToUpdate);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Post post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
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
