using System;
using System.Collections.Generic;
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
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostRelease { get; set; }
        public DateTime PostModified { get; set; }
        public PostFormat PostFormat { get; set; }
        public PostStatus PostStatus { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public int CommentCount { get; set; }

        public int? UserID { get; set; }
        public virtual User User { get; set; }
    }
}