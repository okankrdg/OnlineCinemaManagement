namespace CinemaManagement.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShowId = c.Int(nullable: false),
                        SeatId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Seats", t => t.SeatId, cascadeDelete: true)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: true)
                .Index(t => t.ShowId)
                .Index(t => t.SeatId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Row = c.String(),
                        Number = c.Int(nullable: false),
                        HallId = c.Int(nullable: false),
                        ShowId = c.Int(nullable: false),
                        IsAvaliable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Halls", t => t.HallId, cascadeDelete: true)
                .ForeignKey("dbo.Shows", t => t.ShowId, cascadeDelete: false)
                .Index(t => t.HallId)
                .Index(t => t.ShowId);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HallId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Halls", t => t.HallId, cascadeDelete: false)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.HallId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Director = c.String(),
                        Cast = c.String(),
                        Description = c.String(),
                        Duration = c.Int(nullable: false),
                        PosterPhotoPath = c.String(),
                        PublishedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.Tickets", "SeatId", "dbo.Seats");
            DropForeignKey("dbo.Seats", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.Shows", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Halls", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Shows", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Seats", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Tickets", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Shows", new[] { "MovieId" });
            DropIndex("dbo.Shows", new[] { "HallId" });
            DropIndex("dbo.Halls", new[] { "Movie_Id" });
            DropIndex("dbo.Seats", new[] { "ShowId" });
            DropIndex("dbo.Seats", new[] { "HallId" });
            DropIndex("dbo.Tickets", new[] { "CustomerId" });
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            DropIndex("dbo.Tickets", new[] { "ShowId" });
            DropTable("dbo.Movies");
            DropTable("dbo.Shows");
            DropTable("dbo.Halls");
            DropTable("dbo.Seats");
            DropTable("dbo.Tickets");
            DropTable("dbo.Customers");
            DropTable("dbo.Admins");
        }
    }
}
