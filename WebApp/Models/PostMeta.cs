using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PostMeta
    {
        public int PostMetaID { get; set; }

        public string MetaKey { get; set; }
        public string MetaValue { get; set; }

        public int? PostID { get; set; }
        public virtual Post Post { get; set; }
    }
}