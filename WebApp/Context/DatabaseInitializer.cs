using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApp.Models;

namespace WebApp.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var users = new List<User>
            {
                new User { UserName="ntk132", UserPass="", UserBio="", UserRelease=DateTime.Now, Email="ngtkie1302@gmail.com", Url="", Links=""},
                new User { UserName="nth277", UserPass="", UserBio="", UserRelease=DateTime.Now, Email="nguyenthhu277@example.com", Url="ngthhu.com.example", Links=""},
                new User { UserName="nhuy188", UserPass="", UserBio="Suzy", UserRelease=DateTime.Now, Email="", Url="suzyjen.example.org", Links=""},
                new User { UserName="mn077", UserPass="", UserBio="Cindy 77", UserRelease=DateTime.Now, Email="", Url="cindy.may.com", Links=""},
                new User { UserName="hnhu209", UserPass="", UserBio="", UserRelease=DateTime.Now, Email="", Url="nnhn.example.org", Links=""},
                new User { UserName="fk222", UserPass="", UserBio="Frank Kelly", UserRelease=DateTime.Now, Email="fkelly222@fk.example.com", Url="", Links=""},
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var usermetas = new List<UserMeta>
            {
                new UserMeta { UserID=1, MetaKey="avatar", MetaValue="/Images/Avatar/team1.jpg"},
                new UserMeta { UserID=2, MetaKey="avatar", MetaValue="/Images/Avatar/team2.jpg"},
                new UserMeta { UserID=3, MetaKey="avatar", MetaValue="/Images/Avatar/team3.jpg"},
                new UserMeta { UserID=4, MetaKey="avatar", MetaValue="/Images/Avatar/team4.jpg"},
                new UserMeta { UserID=5, MetaKey="avatar", MetaValue="/Images/Avatar/team5.jpg"},
            };

            usermetas.ForEach(u => context.UserMetas.Add(u));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post { PostName="", PostTitle="Lesson 1: ", PostContent="No matter which side of the “are blogs social media?” debate you fall on, it’s a good idea to share your blog posts on your social networks. You don’t have to limit yourself to new posts either. Sharing older, but popular posts can help you get more mileage out of existing content.", PostRelease=DateTime.Now, PostModified=DateTime.Now, PostFormat=PostFormat.Standard, PostStatus=PostStatus.Publish, CommentStatus=CommentStatus.Open, CommentCount=0, UserID=1},
                new Post { PostName="", PostTitle="Lesson 2: ", PostContent="Show just how much your customers love your company by sharing testimonials on your social media pages. These can either be more traditional printed case studies or even just a customer quote turned into an image. Testimonial videos are especially effective at communicating customer stories.", PostRelease=DateTime.Now, PostModified=DateTime.Now, PostFormat=PostFormat.Standard, PostStatus=PostStatus.Publish, CommentStatus=CommentStatus.Open, CommentCount=0, UserID=1},
                new Post { PostName="", PostTitle="Lesson 3: ", PostContent="Company and product announcements have a place on social, but be sure to use this type of content sparingly so as not to seem overly promotional. Share new product developments to generate excitement and demand, or post news like company award wins to show how your business is exceling or growing.", PostRelease=DateTime.Now, PostModified=DateTime.Now, PostFormat=PostFormat.Standard, PostStatus=PostStatus.Trash, CommentStatus=CommentStatus.Closed, CommentCount=0, UserID=1},
                new Post { PostName="", PostTitle="Part 1: ", PostContent="Is your company hosting a webinar or attending the next big industry tradeshow? Share the news on social. Photos from in-person events are particularly effective at driving engagement, both online and off.", PostRelease=DateTime.Now, PostModified=DateTime.Now, PostFormat=PostFormat.Standard, PostStatus=PostStatus.Draft, CommentStatus=CommentStatus.Closed, CommentCount=0, UserID=4},
                new Post { PostName="", PostTitle="Part 2: ", PostContent="Whether it’s a glimpse into your product or people, behind-the-scenes photos and videos make for effective social media content. They help humanize your business and can help customers feel more connected with your brand. ", PostRelease=DateTime.Now, PostModified=DateTime.Now, PostFormat=PostFormat.Gallery, PostStatus=PostStatus.Trash, CommentStatus=CommentStatus.Open, CommentCount=0, UserID=3},
                new Post { PostName="", PostTitle="Season 3: ", PostContent="Much like tips, quotes work well because they are quick, shareable and can be placed on an image to drive visual interest. While famous quotes are the more popular way to go, you could also opt to share insights from your company leadership about your business or industry. ", PostRelease=DateTime.Now, PostModified=DateTime.Now, PostFormat=PostFormat.Image, PostStatus=PostStatus.Publish, CommentStatus=CommentStatus.Closed, CommentCount=0, UserID=6},
            };

            posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();

            var postmetas = new List<PostMeta>
            {
                new PostMeta { PostID=1, MetaKey="img_thumbnail", MetaValue="/Images/Food/bread.jpg"},
                new PostMeta { PostID=2, MetaKey="img_thumbnail", MetaValue="/Images/Food/cherries.jpg"},
                new PostMeta { PostID=3, MetaKey="img_thumbnail", MetaValue="/Images/Food/salmon.jpg"},
                new PostMeta { PostID=4, MetaKey="img_thumbnail", MetaValue="/Images/Food/wine.jpg"},
                new PostMeta { PostID=5, MetaKey="img_thumbnail", MetaValue="/Images/Food/steak.jpg"},
                new PostMeta { PostID=6, MetaKey="img_thumbnail", MetaValue="/Images/Food/popsicle.jpg"},
            };

            postmetas.ForEach(p => context.PostMetas.Add(p));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product { ProductName="Pepsi", ProductInfo="", ProductRelease=DateTime.Now, ProductModified=DateTime.Now, ProductStatus=ProductStatus.Available, Price=Convert.ToDecimal(4.50), UserID=2 },
                new Product { ProductName="Coca", ProductInfo="", ProductRelease=DateTime.Now, ProductModified=DateTime.Now, ProductStatus=ProductStatus.Available, Price=Convert.ToDecimal(4.99), UserID=3 },
                new Product { ProductName="Pizza", ProductInfo="Processed pizza", ProductRelease=DateTime.Now, ProductModified=DateTime.Now, ProductStatus=ProductStatus.SoldOut, Price=Convert.ToDecimal(45.00), UserID=2 },
                new Product { ProductName="Snack", ProductInfo="Small size", ProductRelease=DateTime.Now, ProductModified=DateTime.Now, ProductStatus=ProductStatus.SoldOut, Price=Convert.ToDecimal(1.25), UserID=4 },
                new Product { ProductName="Noodles", ProductInfo="", ProductRelease=DateTime.Now, ProductModified=DateTime.Now, ProductStatus=ProductStatus.Available, Price=Convert.ToDecimal(10.99), UserID=5 },
                new Product { ProductName="Coffee", ProductInfo="Without milk", ProductRelease=DateTime.Now, ProductModified=DateTime.Now, ProductStatus=ProductStatus.SoldOut, Price=Convert.ToDecimal(5.99), UserID=6 },
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
            
        }
    }
}