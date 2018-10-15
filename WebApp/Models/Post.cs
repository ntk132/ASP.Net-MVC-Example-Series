using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum PostFormat
    {
        Standard,
        Gallery,
        Audio,
        Link,
        Image,
        Quote,
        Video,
    }

    public enum PostStatus
    {
        Publish,
        Draft,
        Trash,
    }

    public enum CommentStatus
    {
        Open,
        Closed,
    }

    public class Post
    {
        public int PostID { get; set; }
        public string PostName { get; set; }

        [Display(Name = "Title")]
        public string PostTitle { get; set; }

        [Display(Name = "Content")]
        public string PostContent { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostRelease { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostModified { get; set; }

        [Display(Name = "Format")]
        public PostFormat PostFormat { get; set; }

        [Display(Name = "Status")]
        public PostStatus PostStatus { get; set; }

        [Display(Name = "Comment Status")]
        public CommentStatus CommentStatus { get; set; }

        [Display(Name = "Comment Count")]
        public int CommentCount { get; set; }

        public int? UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostMeta> PostMetas { get; set; }
    }
}