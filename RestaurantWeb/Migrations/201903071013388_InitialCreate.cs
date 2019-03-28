namespace RestaurantWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    //public partial class InitialCreate : DbMigration
    //{
    //    public override void Up()
    //    {
    //        CreateTable(
    //            "dbo.Areas",
    //            c => new
    //                {
    //                    ID = c.Int(nullable: false, identity: true),
    //                    Name = c.String(),
    //                })
    //            .PrimaryKey(t => t.ID);
            
    //        CreateTable(
    //            "dbo.Categories",
    //            c => new
    //                {
    //                    ID = c.Int(nullable: false, identity: true),
    //                    Name = c.String(),
    //                })
    //            .PrimaryKey(t => t.ID);
            
    //        CreateTable(
    //            "dbo.Products",
    //            c => new
    //                {
    //                    ID = c.Int(nullable: false, identity: true),
    //                    Name = c.String(),
    //                    CategoryID = c.Int(nullable: false),
    //                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
    //                })
    //            .PrimaryKey(t => t.ID)
    //            .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
    //            .Index(t => t.CategoryID);
            
    //        CreateTable(
    //            "dbo.SoldProducts",
    //            c => new
    //                {
    //                    ID = c.Int(nullable: false, identity: true),
    //                    Name = c.String(),
    //                    CategoryID = c.Int(nullable: false),
    //                    TableID = c.Int(nullable: false),
    //                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
    //                    Detail = c.String(),
    //                })
    //            .PrimaryKey(t => t.ID)
    //            .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
    //            .ForeignKey("dbo.Tables", t => t.TableID, cascadeDelete: true)
    //            .Index(t => t.CategoryID)
    //            .Index(t => t.TableID);
            
    //        CreateTable(
    //            "dbo.Tables",
    //            c => new
    //                {
    //                    ID = c.Int(nullable: false, identity: true),
    //                    NumberOfTable = c.Int(nullable: false),
    //                    AreaID = c.Int(nullable: false),
    //                    Occupied = c.Boolean(nullable: false),
    //                })
    //            .PrimaryKey(t => t.ID)
    //            .ForeignKey("dbo.Areas", t => t.AreaID, cascadeDelete: true)
    //            .Index(t => t.AreaID);
            
    //    }
        
    //    public override void Down()
    //    {
    //        DropForeignKey("dbo.SoldProducts", "TableID", "dbo.Tables");
    //        DropForeignKey("dbo.Tables", "AreaID", "dbo.Areas");
    //        DropForeignKey("dbo.SoldProducts", "CategoryID", "dbo.Categories");
    //        DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
    //        DropIndex("dbo.Tables", new[] { "AreaID" });
    //        DropIndex("dbo.SoldProducts", new[] { "TableID" });
    //        DropIndex("dbo.SoldProducts", new[] { "CategoryID" });
    //        DropIndex("dbo.Products", new[] { "CategoryID" });
    //        DropTable("dbo.Tables");
    //        DropTable("dbo.SoldProducts");
    //        DropTable("dbo.Products");
    //        DropTable("dbo.Categories");
    //        DropTable("dbo.Areas");
    //    }
    //}
}
