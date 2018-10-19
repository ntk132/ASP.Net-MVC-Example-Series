using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum CommentType
    {
        Comment,
        Edit,
        Trash
    }

    public class Comment
    {
        public int CommentID { get; set; }

        [Display(Name = "Author")]
        public string CommentAuthor { get; set; }

        [Display(Name = "Enmail")]
        public string CommentAuthorEmail { get; set; }

        [Display(Name = "Released")]
        public string CommentDateReleased { get; set; }

        [Display(Name = "Modified")]
        public string CommentDateModified { get; set; }
        public string CommentContent { get; set; }
        public CommentType CommentType { get; set; }
        public int CommentParent { get; set; }


        public int? UserID { get; set; }
        public virtual User User { get; set; }
        public int? PostID { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<CommentMeta> CommentMetas { get; set; }
    }
}