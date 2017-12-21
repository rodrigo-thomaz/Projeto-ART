namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNameInDataSheet1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HardwaresInProject", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.HardwaresInProject", new[] { "ApplicationId", "DeviceId" }, "dbo.HardwareInApplication");
            DropForeignKey("dbo.Project", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.HardwaresInProject", "ProjectId", "dbo.Project");
            DropIndex("dbo.HardwaresInProject", "IX_Unique_HardwareInApplication_ProjectId");
            DropIndex("dbo.HardwaresInProject", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.Project", new[] { "ApplicationId" });
            DropIndex("dbo.Project", new[] { "CreateByApplicationUserId" });
            DropTable("dbo.HardwaresInProject");
            DropTable("dbo.Project");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        ApplicationId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HardwaresInProject",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ApplicationId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Project", "CreateByApplicationUserId");
            CreateIndex("dbo.Project", "ApplicationId");
            CreateIndex("dbo.HardwaresInProject", "CreateByApplicationUserId");
            CreateIndex("dbo.HardwaresInProject", new[] { "ApplicationId", "DeviceId", "ProjectId" }, unique: true, name: "IX_Unique_HardwareInApplication_ProjectId");
            AddForeignKey("dbo.HardwaresInProject", "ProjectId", "dbo.Project", "Id");
            AddForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.Project", "ApplicationId", "dbo.Application", "Id");
            AddForeignKey("dbo.HardwaresInProject", new[] { "ApplicationId", "DeviceId" }, "dbo.HardwareInApplication", new[] { "ApplicationId", "DeviceId" });
            AddForeignKey("dbo.HardwaresInProject", "CreateByApplicationUserId", "dbo.ApplicationUser", "Id", cascadeDelete: true);
        }
    }
}
