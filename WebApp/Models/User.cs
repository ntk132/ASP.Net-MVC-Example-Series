using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "User Pass")]
        public string UserPass { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyy}", ApplyFormatInEditMode = true)]
        public DateTime UserRelease { get; set; }

        [Display(Name = "Description")]
        public string UserBio { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Links { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<UserMeta> UserMetas { get; set; }
    }
}