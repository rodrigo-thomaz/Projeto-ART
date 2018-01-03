namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceWiFi",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Short(nullable: false),
                        HostName = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceWiFi", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropIndex("dbo.DeviceWiFi", new[] { "Id", "DeviceDatasheetId" });
            DropTable("dbo.DeviceWiFi");
        }
    }
}
