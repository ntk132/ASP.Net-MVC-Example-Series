using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TermMeta
    {
        public int TermMetaID { get; set; }

        public string MetaKey { get; set; }
        public string MetaValue { get; set; }

        public int TermID { get; set; }
        public virtual Term Term { get; set; }
    }
}