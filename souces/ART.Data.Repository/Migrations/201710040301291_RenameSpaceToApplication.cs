namespace ART.Data.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameSpaceToApplication : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateTable(
                "dbo.UserInSpace",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SpaceId });

            CreateTable(
                "dbo.Space",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.HardwareInSpace",
                c => new
                    {
                        HardwareBaseId = c.Guid(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.HardwareBaseId, t.SpaceId });

            DropForeignKey("dbo.UserInApplication", "UserId", "dbo.User");
            DropForeignKey("dbo.UserInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.HardwareInApplication", "HardwareBaseId", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwareInApplication", "ApplicationId", "dbo.Application");
            DropIndex("dbo.UserInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.UserInApplication", new[] { "UserId" });
            DropIndex("dbo.HardwareInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.HardwareInApplication", new[] { "HardwareBaseId" });
            DropIndex("dbo.Application", new[] { "Name" });
            DropTable("dbo.UserInApplication");
            DropTable("dbo.HardwareInApplication");
            DropTable("dbo.Application");
            CreateIndex("dbo.UserInSpace", "SpaceId");
            CreateIndex("dbo.UserInSpace", "UserId");
            CreateIndex("dbo.Space", "Name", unique: true);
            CreateIndex("dbo.HardwareInSpace", "SpaceId");
            CreateIndex("dbo.HardwareInSpace", "HardwareBaseId");
            AddForeignKey("dbo.HardwareInSpace", "SpaceId", "dbo.Space", "Id");
            AddForeignKey("dbo.UserInSpace", "UserId", "dbo.User", "Id");
            AddForeignKey("dbo.UserInSpace", "SpaceId", "dbo.Space", "Id");
            AddForeignKey("dbo.HardwareInSpace", "HardwareBaseId", "dbo.HardwareBase", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.HardwareInSpace", "HardwareBaseId", "dbo.HardwareBase");
            DropForeignKey("dbo.UserInSpace", "SpaceId", "dbo.Space");
            DropForeignKey("dbo.UserInSpace", "UserId", "dbo.User");
            DropForeignKey("dbo.HardwareInSpace", "SpaceId", "dbo.Space");
            DropIndex("dbo.HardwareInSpace", new[] { "HardwareBaseId" });
            DropIndex("dbo.HardwareInSpace", new[] { "SpaceId" });
            DropIndex("dbo.Space", new[] { "Name" });
            DropIndex("dbo.UserInSpace", new[] { "UserId" });
            DropIndex("dbo.UserInSpace", new[] { "SpaceId" });
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
                "dbo.HardwareInApplication",
                c => new
                    {
                        HardwareBaseId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.HardwareBaseId, t.ApplicationId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.HardwareBase", t => t.HardwareBaseId)
                .Index(t => t.HardwareBaseId)
                .Index(t => t.ApplicationId);

            CreateTable(
                "dbo.UserInApplication",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ApplicationId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);

            DropTable("dbo.HardwareInSpace");
            DropTable("dbo.Space");
            DropTable("dbo.UserInSpace");
        }

        #endregion Methods
    }
}