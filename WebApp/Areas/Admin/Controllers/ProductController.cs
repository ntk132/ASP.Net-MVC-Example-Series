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
    public class ProductController : UploadController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Product
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

            var products = from p in db.Products select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_decs":
                    products = products.OrderByDescending(u => u.ProductName);
                    break;
                case "Date":
                    products = products.OrderBy(u => u.ProductModified);
                    break;
                case "date_decs":
                    products = products.OrderByDescending(u => u.ProductModified);
                    break;
                default:
                    products = products.OrderBy(u => u.ProductName);
                    break;
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");

            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductName,ProductInfo,ProductRelease,ProductModified,ProductStatus,Price,UserID")] Product product)
        {
            ProductMeta productMeta = new ProductMeta();
            var icon = Request.Form["icon"];

            try
            {
                if (ModelState.IsValid)
                {
                    product.ProductRelease = DateTime.Now;
                    product.ProductModified = DateTime.Now;

                    db.Products.Add(product);

                    productMeta.MetaKey = "img_icon";
                    productMeta.MetaValue = icon;
                    productMeta.ProductID = product.ProductID;

                    db.SaveChanges();

                    product.ProductMetas.Add(productMeta);

                    db.SaveChanges();

                    return RedirectToAction("Edit", new { id = product.ProductID });
                }
            }
            catch (DataException /*de*/)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.Icon = icon;
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", product.UserID);

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);
            ProductMeta productMeta = product.ProductMetas.Where(m => m.MetaKey == "img_icon").Single();

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Icon = productMeta.MetaValue;
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", product.UserID);

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productToUpdate = db.Products.Find(id);
            var productMetaToUpdate = productToUpdate.ProductMetas.Where(m => m.MetaKey == "img_icon").Single();
            var icon = Request.Form["icon"];

            if (TryUpdateModel(productToUpdate, "", new string[] { "ProductName", "ProductInfo", "ProductModified", "ProductStatus", "Price" }))
            {
                try
                {
                    productToUpdate.ProductModified = DateTime.Now;
                    productMetaToUpdate.MetaValue = icon;

                    db.SaveChanges();
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewBag.Icon = icon;
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", productToUpdate.UserID);

            return View(productToUpdate);
        }

        // GET: Product/Delete/5
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
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = db.Products.Find(id);

                foreach (ProductMeta productMeta in product.ProductMetas)
                {
                    db.ProductMetas.Remove(productMeta);
                }

                db.Products.Remove(product);
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
