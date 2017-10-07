namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HardwaresInApplication", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.HardwaresInApplication", "CreateByApplicationUserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.HardwaresInApplication", "CreateByApplicationUserId");
            AddForeignKey("dbo.HardwaresInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HardwaresInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropIndex("dbo.HardwaresInApplication", new[] { "CreateByApplicationUserId" });
            DropColumn("dbo.HardwaresInApplication", "CreateByApplicationUserId");
            DropColumn("dbo.HardwaresInApplication", "CreateDate");
        }
    }
}
