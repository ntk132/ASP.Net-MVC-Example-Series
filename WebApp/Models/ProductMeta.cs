using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ProductMeta
    {
        public int ProductMetaID { get; set; }        
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }

        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}