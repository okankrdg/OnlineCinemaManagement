namespace CinemaManagement.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatShowIdChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "ShowId", "dbo.Shows");
            DropIndex("dbo.Seats", new[] { "ShowId" });
            AlterColumn("dbo.Seats", "ShowId", c => c.Int());
            CreateIndex("dbo.Seats", "ShowId");
            AddForeignKey("dbo.Seats", "ShowId", "dbo.Shows", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seats", "ShowId", "dbo.Shows");
            DropIndex("dbo.Seats", new[] { "ShowId" });
            AlterColumn("dbo.Seats", "ShowId", c => c.Int(nullable: false));
            CreateIndex("dbo.Seats", "ShowId");
            AddForeignKey("dbo.Seats", "ShowId", "dbo.Shows", "Id", cascadeDelete: true);
        }
    }
}
