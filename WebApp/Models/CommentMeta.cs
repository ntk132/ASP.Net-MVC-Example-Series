using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CommentMeta
    {
        public int CommentMetaID { get; set; }

        public string MetaKey { get; set; }
        public string MetaValue { get; set; }

        public int? CommentID { get; set; }
        public virtual Comment Comment { get; set; }
    }
}