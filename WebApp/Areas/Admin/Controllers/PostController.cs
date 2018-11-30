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

namespace WebApp.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class PostController : UploadController
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
        public ActionResult Create([Bind(Include = "PostTitle,PostContent,PostFormat,PostStatus,CommentStatus,UserID")] Post post)
        {
            PostMeta postMeta = new PostMeta();
            var thumbnail = Request.Form["upload"];

            try
            {
                if (ModelState.IsValid)
                {
                    // Default datas - which are automatically defined
                    post.PostRelease = DateTime.Now;
                    post.PostModified = DateTime.Now;
                    post.CommentCount = 0;
                    post.PostContent.Replace("\n", "<br />");
                    post.User = db.Users.Where(u => u.UserID == post.UserID).Single();

                    // Store post to DB
                    db.Posts.Add(post);
                    db.SaveChanges();

                    // Prepare PostMeta's values
                    postMeta.PostID = post.PostID;
                    postMeta.MetaKey = "img_thumbnail";
                    postMeta.MetaValue = thumbnail;

                    // Store PostMeta to DB
                    db.PostMetas.Add(postMeta);
                    db.SaveChanges();

                    // Prepare Post's terms


                    // Store Terms to DB


                    // Direct to Edit page follow this Post's ID
                    return RedirectToAction("Edit", new { id = post.PostID });
                }
            }
            catch (DataException /*de*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.Thumbnail = thumbnail;
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

            ViewBag.Thumbnail = db.PostMetas.Where(m => m.PostID == id.Value && m.MetaKey == "img_thumbnail").Single().MetaValue;
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
            var postMetaToUpdate = db.PostMetas.Where(m => m.PostID ==id.Value && m.MetaKey == "img_thumbnail").Single();
            var thumbnail = Request.Form["upload"];

            if (TryUpdateModel(postToUpdate, "", new string[] { "PostTitle", "PostContent", "PostFormat", "PostStatus", "CommentStatus" }))
            {                
                postToUpdate.PostModified = DateTime.Now;
                postMetaToUpdate.MetaValue = thumbnail;

                db.SaveChanges();
            }

            ViewBag.Thumbnail = thumbnail;
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

                // Delete all meta of this post
                foreach(PostMeta postMeta in db.PostMetas.Where(m => m.PostID == id))
                {
                    db.PostMetas.Remove(postMeta);
                }

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
