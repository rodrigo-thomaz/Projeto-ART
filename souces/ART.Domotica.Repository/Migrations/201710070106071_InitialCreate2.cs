namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "CreateByApplicationUserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Project", "CreateByApplicationUserId");
            AddForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropIndex("dbo.Project", new[] { "CreateByApplicationUserId" });
            DropColumn("dbo.Project", "CreateByApplicationUserId");
        }
    }
}
