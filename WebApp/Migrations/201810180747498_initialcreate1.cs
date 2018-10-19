namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TermMeta",
                c => new
                    {
                        TermMetaID = c.Int(nullable: false, identity: true),
                        MetaKey = c.String(),
                        MetaValue = c.String(),
                        TermID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TermMetaID)
                .ForeignKey("dbo.Term", t => t.TermID, cascadeDelete: true)
                .Index(t => t.TermID);
            
            CreateTable(
                "dbo.Term",
                c => new
                    {
                        TermID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.TermID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TermMeta", "TermID", "dbo.Term");
            DropIndex("dbo.TermMeta", new[] { "TermID" });
            DropTable("dbo.Term");
            DropTable("dbo.TermMeta");
        }
    }
}
