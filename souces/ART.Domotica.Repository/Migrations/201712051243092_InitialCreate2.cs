namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HardwaresInProject", "DeviceInApplicationId", "dbo.DeviceInApplication");
            DropIndex("dbo.DeviceInApplication", "IX_Unique_ApplicationId_DeviceBaseId");
            DropIndex("dbo.HardwaresInProject", "IX_Unique_DeviceInApplicationId_ProjectId");
            RenameColumn(table: "dbo.HardwaresInProject", name: "DeviceInApplicationId", newName: "ApplicationId");
            DropPrimaryKey("dbo.DeviceInApplication");
            AddColumn("dbo.HardwaresInProject", "DeviceBaseId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.DeviceInApplication", new[] { "ApplicationId", "DeviceBaseId" });
            CreateIndex("dbo.DeviceInApplication", "ApplicationId");
            CreateIndex("dbo.HardwaresInProject", new[] { "ApplicationId", "DeviceBaseId", "ProjectId" }, unique: true, name: "IX_Unique_DeviceInApplication_ProjectId");
            AddForeignKey("dbo.HardwaresInProject", new[] { "ApplicationId", "DeviceBaseId" }, "dbo.DeviceInApplication", new[] { "ApplicationId", "DeviceBaseId" });
            DropColumn("dbo.DeviceInApplication", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceInApplication", "Id", c => c.Guid(nullable: false, identity: true));
            DropForeignKey("dbo.HardwaresInProject", new[] { "ApplicationId", "DeviceBaseId" }, "dbo.DeviceInApplication");
            DropIndex("dbo.HardwaresInProject", "IX_Unique_DeviceInApplication_ProjectId");
            DropIndex("dbo.DeviceInApplication", new[] { "ApplicationId" });
            DropPrimaryKey("dbo.DeviceInApplication");
            DropColumn("dbo.HardwaresInProject", "DeviceBaseId");
            AddPrimaryKey("dbo.DeviceInApplication", "Id");
            RenameColumn(table: "dbo.HardwaresInProject", name: "ApplicationId", newName: "DeviceInApplicationId");
            CreateIndex("dbo.HardwaresInProject", new[] { "DeviceInApplicationId", "ProjectId" }, unique: true, name: "IX_Unique_DeviceInApplicationId_ProjectId");
            CreateIndex("dbo.DeviceInApplication", new[] { "ApplicationId", "DeviceBaseId" }, unique: true, name: "IX_Unique_ApplicationId_DeviceBaseId");
            AddForeignKey("dbo.HardwaresInProject", "DeviceInApplicationId", "dbo.DeviceInApplication", "Id");
        }
    }
}
