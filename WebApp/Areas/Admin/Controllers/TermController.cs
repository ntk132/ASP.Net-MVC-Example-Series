using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class TermController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Admin/Term
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTerm(String input)
        {
            String[] data = input.Split(':');
            Term term = new Term();
            TermMeta meta = new TermMeta();

            term.Name = data[1];
            term.Slug = data[1].ToLower().Replace(" ", "_");

            meta.MetaKey = "term_type";
            meta.MetaValue = data[0];

            term.TermMetas.Add(meta);

            db.Terms.Add(term);
            db.SaveChanges();

            meta.TermID = term.TermID;
            meta.Term = term;

            db.TermMetas.Add(meta);
            db.SaveChanges();

            return View();
        }

        public void AddRelation(Post post, Term[] terms)
        {
            foreach (Term term in terms)
            {
                TermRelation relation = new TermRelation();

                relation.PostID = post.PostID;
                relation.TermID = term.TermID;

                db.TermRelations.Add(relation);

                db.SaveChanges();
            }
        }

        public void DeleteRelation(Post post, Term term)
        {
            TermRelation relation = db.TermRelations.Where(tr => tr.PostID == post.PostID && tr.TermID == term.TermID).Single();

            db.TermRelations.Remove(relation);

            db.SaveChanges();
        }
    }
}