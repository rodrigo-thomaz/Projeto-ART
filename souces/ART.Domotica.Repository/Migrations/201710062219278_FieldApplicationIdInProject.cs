namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldApplicationIdInProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "ApplicationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Project", "ApplicationId");
            AddForeignKey("dbo.Project", "ApplicationId", "dbo.Application", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "ApplicationId", "dbo.Application");
            DropIndex("dbo.Project", new[] { "ApplicationId" });
            DropColumn("dbo.Project", "ApplicationId");
        }
    }
}
