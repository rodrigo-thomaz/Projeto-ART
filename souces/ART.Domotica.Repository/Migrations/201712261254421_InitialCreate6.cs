namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceBinary",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceBinary", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropIndex("dbo.DeviceBinary", new[] { "Id", "DeviceDatasheetId" });
            DropTable("dbo.DeviceBinary");
        }
    }
}
