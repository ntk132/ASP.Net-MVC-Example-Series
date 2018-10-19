using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Term
    {
        public int TermID { get; set; }

        // Into the name of term, it's enable to have Upper word, space,...
        public string Name { get; set; }

        // Into the slug of term, it's only accept to use lower letter and '_' mark.
        public string Slug { get; set; }

        public virtual ICollection<TermMeta> TermMetas { get; set; }
    }
}