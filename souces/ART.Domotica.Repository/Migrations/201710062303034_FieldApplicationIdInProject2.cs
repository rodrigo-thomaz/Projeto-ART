namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldApplicationIdInProject2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HardwaresInApplication", new[] { "HardwareBaseId" });
            DropIndex("dbo.HardwaresInApplication", new[] { "ApplicationId" });
            DropPrimaryKey("dbo.HardwaresInApplication");
            AddColumn("dbo.HardwaresInApplication", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.HardwaresInApplication", "Id");
            CreateIndex("dbo.HardwaresInApplication", new[] { "ApplicationId", "HardwareBaseId" }, unique: true, name: "IX_Unique_ApplicationId_HardwareBaseId");
            CreateIndex("dbo.HardwaresInApplication", "HardwareBaseId", unique: true, name: "IX_Unique_HardwareBaseId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HardwaresInApplication", "IX_Unique_HardwareBaseId");
            DropIndex("dbo.HardwaresInApplication", "IX_Unique_ApplicationId_HardwareBaseId");
            DropPrimaryKey("dbo.HardwaresInApplication");
            DropColumn("dbo.HardwaresInApplication", "Id");
            AddPrimaryKey("dbo.HardwaresInApplication", new[] { "HardwareBaseId", "ApplicationId" });
            CreateIndex("dbo.HardwaresInApplication", "ApplicationId");
            CreateIndex("dbo.HardwaresInApplication", "HardwareBaseId");
        }
    }
}
