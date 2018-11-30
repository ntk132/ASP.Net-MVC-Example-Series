using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*bool log = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (log)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }*/

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult PageCarousel()
        {
            return View();
        }

        public ActionResult PageProduct()
        {
            return View();
        }
        public ActionResult PageBlog()
        {
            return View();
        }
        public ActionResult PageContact()
        {
            return View();
        }
        public ActionResult PageAbout()
        {
            return View();
        }
        public ActionResult PagePricing()
        {
            return View();
        }
        public ActionResult PageLanding()
        {
            return View();
        }

        public ActionResult CardBlog()
        {
            return View();
        }
        public ActionResult CardProfile()
        {
            return View();
        }
        public ActionResult CardBG()
        {
            return View();
        }
        public ActionResult CardPricing()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }
        public ActionResult BlogPage()
        {
            return View();
        }
        public ActionResult BlogSingle()
        {
            return View();
        }
        public ActionResult ProductPage()
        {
            return View();
        }
        public ActionResult ProductSingle()
        {
            return View();
        }
    }
}