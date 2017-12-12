namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorInApplication",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        SensorId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.SensorId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId)
                .ForeignKey("dbo.Sensor", t => t.SensorId)
                .Index(t => t.ApplicationId)
                .Index(t => t.SensorId, unique: true, name: "IX_Unique_SensorId")
                .Index(t => t.CreateByApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorInApplication", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.SensorInApplication", "ApplicationId", "dbo.Application");
            DropIndex("dbo.SensorInApplication", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.SensorInApplication", "IX_Unique_SensorId");
            DropIndex("dbo.SensorInApplication", new[] { "ApplicationId" });
            DropTable("dbo.SensorInApplication");
        }
    }
}
