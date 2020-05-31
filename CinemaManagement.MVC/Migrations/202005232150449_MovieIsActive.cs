namespace CinemaManagement.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "IsActive");
        }
    }
}
