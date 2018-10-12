namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostMeta",
                c => new
                    {
                        PostMetaID = c.Int(nullable: false, identity: true),
                        MetaKey = c.String(),
                        MetaValue = c.String(),
                        PostID = c.Int(),
                    })
                .PrimaryKey(t => t.PostMetaID)
                .ForeignKey("dbo.Post", t => t.PostID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PostName = c.String(),
                        PostTitle = c.String(),
                        PostContent = c.String(),
                        PostRelease = c.DateTime(nullable: false),
                        PostModified = c.DateTime(nullable: false),
                        PostFormat = c.Int(nullable: false),
                        PostStatus = c.Int(nullable: false),
                        CommentStatus = c.Int(nullable: false),
                        CommentCount = c.Int(nullable: false),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPass = c.String(),
                        UserRelease = c.DateTime(nullable: false),
                        UserBio = c.String(),
                        Email = c.String(),
                        Url = c.String(),
                        Links = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductInfo = c.String(),
                        ProductRelease = c.DateTime(nullable: false),
                        ProductModified = c.DateTime(nullable: false),
                        ProductStatus = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ProductMeta",
                c => new
                    {
                        ProductMetaID = c.Int(nullable: false, identity: true),
                        MetaKey = c.String(),
                        MetaValue = c.String(),
                        ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductMetaID)
                .ForeignKey("dbo.Product", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.UserMeta",
                c => new
                    {
                        UserMetaID = c.Int(nullable: false, identity: true),
                        MetaKey = c.String(),
                        MetaValue = c.String(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.UserMetaID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostMeta", "PostID", "dbo.Post");
            DropForeignKey("dbo.UserMeta", "UserID", "dbo.User");
            DropForeignKey("dbo.Product", "UserID", "dbo.User");
            DropForeignKey("dbo.ProductMeta", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Post", "UserID", "dbo.User");
            DropIndex("dbo.UserMeta", new[] { "UserID" });
            DropIndex("dbo.ProductMeta", new[] { "ProductID" });
            DropIndex("dbo.Product", new[] { "UserID" });
            DropIndex("dbo.Post", new[] { "UserID" });
            DropIndex("dbo.PostMeta", new[] { "PostID" });
            DropTable("dbo.UserMeta");
            DropTable("dbo.ProductMeta");
            DropTable("dbo.Product");
            DropTable("dbo.User");
            DropTable("dbo.Post");
            DropTable("dbo.PostMeta");
        }
    }
}
