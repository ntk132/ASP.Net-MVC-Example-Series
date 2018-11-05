using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class UserController : UploadController
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

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,UserPass,UserBio,Email,Url,Links")] User user)
        {
            var array = new string[] { Request.Form["fb"], Request.Form["yt"], Request.Form["ins"], Request.Form["web"] };
            var newArray = array.Select(x => new { link = x }).ToArray();

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(newArray); // Convert to JSON

            // create meta to store avatar of new user
            var avatar = Request.Form["avatar"];
            UserMeta userMeta = new UserMeta();

            try
            {
                if (ModelState.IsValid)
                {
                    // Store new user to DB
                    user.UserRelease = DateTime.Now;
                    user.Links = json;
                    db.Users.Add(user);
                    db.SaveChanges();

                    // Store UserMate prefer to newest (just created) user
                    userMeta.MetaKey = "avatar";
                    userMeta.MetaValue = avatar;
                    userMeta.UserID = user.UserID;
                    db.UserMetas.Add(userMeta);

                    // Update DB
                    db.SaveChanges();

                    user.UserMetas.Add(userMeta);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException /*de*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Convert Links (JSON) to array string to show multi social links
            string[] result = { "", "", "", "" };

            if (user.Links.ToString() != "")
            {
                // Process the JSON to array string
                string pattern = user.Links.ToString().Replace("\"link\":", "");
                pattern = Regex.Replace(pattern, "[{\":}\\[\\]]", "");
                result = pattern.Split(',');                
            }

            // Store multi links to ViewBag variable for mapping to text box at edit page
            ViewBag.fb = result[0];
            ViewBag.yt = result[1];
            ViewBag.ins = result[2];
            ViewBag.web = result[3];

            // Get user's avatar
            var avatar = db.UserMetas.Where(m => m.MetaKey == "avatar" && m.UserID == id.Value).Single();

            ViewBag.Avatar = avatar.MetaValue;

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int? id, System.Web.Mvc.FormCollection formCollection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userToUpdate = db.Users.Find(id);
            var userMetaToUpdate = db.UserMetas.Where(m => m.MetaKey == "avatar" && m.UserID == id.Value).Single();

            /* Get the JSON from Collecting multi social links */
            // Collect multi social link and store it to array
            var array = new string[] { Request.Form["fb"], Request.Form["yt"], Request.Form["ins"], Request.Form["web"] };
            var newArray = array.Select(x => new { link = x }).ToArray();

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(newArray); // Convert to JSON

            // create meta to store avatar of new user
            var avatar = Request.Form["avatar"];            

            if (TryUpdateModel(userToUpdate, "", new string[] { "UserPass", "UserBio", "Email", "Url", "Links" }))
            {
                
                userToUpdate.Links = json;
                userMetaToUpdate.MetaValue = avatar;
                db.Entry(userToUpdate).State = EntityState.Modified;
                db.Entry(userMetaToUpdate).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(userToUpdate);
        }

        // GET: User/Delete/5
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
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                User user = db.Users.Find(id);

                foreach (UserMeta userMeta in db.UserMetas.Where(m => m.UserID == id))
                {
                    db.UserMetas.Remove(userMeta);
                }

                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }
        /*
        [HttpPost]
        public JsonResult act(String test)
        {
            return Json(new { Message = "NTK132" } , JsonRequestBehavior.AllowGet );
        }

        // GET
        public string LoadMedia()
        {
            string root = "/Images";
            string realRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            List<String> list = LoadFiles(root, realRoot);

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(list); // Convert to JSON

            return json;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    try
                    {
                        if (file.ContentLength > 0)
                        {
                            string _path = Path.Combine(Server.MapPath("~/Images/Desktop/Origin"), file.FileName);
                            file.SaveAs(_path);
                        }

                        ViewBag.Message = "Upload successful!";
                    }
                    catch
                    {
                        ViewBag.Message = "Upload fail!";
                    }
                }
            }

            return Json(new { json = LoadMedia() }, JsonRequestBehavior.AllowGet);
        }

        public List<String> LoadFiles(String path, String realPath)
        {
            DirectoryInfo dir = new DirectoryInfo(realPath);
            List<String> list = new List<string>();

            if (dir.GetDirectories().Count() > 0)
            {
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    list.AddRange(LoadFiles(path + "/" + subDir.Name, Path.Combine(realPath, subDir.Name).ToString()));
                }
            }
            else
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    list.Add(path + "/" + file.Name);
                }
            }

            return list;
        }
        */
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
