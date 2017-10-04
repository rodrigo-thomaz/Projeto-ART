namespace ART.Seguranca.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableApplication : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "Client");
            RenameTable(name: "dbo.RefreshTokens", newName: "RefreshToken");
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.UsersInApplication",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ApplicationId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersInApplication", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersInApplication", "ApplicationId", "dbo.Application");
            DropIndex("dbo.UsersInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.UsersInApplication", new[] { "UserId" });
            DropIndex("dbo.Application", new[] { "Name" });
            DropTable("dbo.UsersInApplication");
            DropTable("dbo.Application");
            RenameTable(name: "dbo.RefreshToken", newName: "RefreshTokens");
            RenameTable(name: "dbo.Client", newName: "Clients");
        }
    }
}
