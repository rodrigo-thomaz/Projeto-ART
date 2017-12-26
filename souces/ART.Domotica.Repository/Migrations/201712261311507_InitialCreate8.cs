namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceBinary", "DeviceDatasheetBinaryId", c => c.Guid(nullable: false));
            CreateIndex("dbo.DeviceBinary", new[] { "DeviceDatasheetBinaryId", "DeviceDatasheetId" });
            AddForeignKey("dbo.DeviceBinary", new[] { "DeviceDatasheetBinaryId", "DeviceDatasheetId" }, "dbo.DeviceDatasheetBinary", new[] { "Id", "DeviceDatasheetId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceBinary", new[] { "DeviceDatasheetBinaryId", "DeviceDatasheetId" }, "dbo.DeviceDatasheetBinary");
            DropIndex("dbo.DeviceBinary", new[] { "DeviceDatasheetBinaryId", "DeviceDatasheetId" });
            DropColumn("dbo.DeviceBinary", "DeviceDatasheetBinaryId");
        }
    }
}
