namespace CinemaManagement.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatReservedAdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "ShowId", "dbo.Shows");
            DropIndex("dbo.Seats", new[] { "ShowId" });
            CreateTable(
                "dbo.SeatReserveds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShowId = c.Int(nullable: false),
                        SeatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seats", t => t.SeatId, cascadeDelete: true)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.ShowId)
                .Index(t => t.SeatId);
            
            DropColumn("dbo.Seats", "ShowId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seats", "ShowId", c => c.Int());
            DropForeignKey("dbo.SeatReserveds", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.SeatReserveds", "SeatId", "dbo.Seats");
            DropIndex("dbo.SeatReserveds", new[] { "SeatId" });
            DropIndex("dbo.SeatReserveds", new[] { "ShowId" });
            DropTable("dbo.SeatReserveds");
            CreateIndex("dbo.Seats", "ShowId");
            AddForeignKey("dbo.Seats", "ShowId", "dbo.Shows", "Id");
        }
    }
}
