namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TermRelation", "PostID");
            CreateIndex("dbo.TermRelation", "TermID");
            AddForeignKey("dbo.TermRelation", "PostID", "dbo.Post", "PostID");
            AddForeignKey("dbo.TermRelation", "TermID", "dbo.Term", "TermID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TermRelation", "TermID", "dbo.Term");
            DropForeignKey("dbo.TermRelation", "PostID", "dbo.Post");
            DropIndex("dbo.TermRelation", new[] { "TermID" });
            DropIndex("dbo.TermRelation", new[] { "PostID" });
        }
    }
}
