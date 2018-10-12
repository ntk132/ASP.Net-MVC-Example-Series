using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserMeta
    {
        public int UserMetaID { get; set; }
        
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }

        public int? UserID { get; set; }
        public virtual User User { get; set; }
    }
}