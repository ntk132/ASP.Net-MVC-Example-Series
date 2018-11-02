using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TermRelation
    {
        [Key]
        public int TermrelationID { get; set; }

        public int? PostID { get; set; }
        public virtual Post Post { get; set; }
        public int? TermID { get; set; }
        public virtual Term Term { get; set; }
    }
}