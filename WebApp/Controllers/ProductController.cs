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
    public class ProductController : Controller
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
