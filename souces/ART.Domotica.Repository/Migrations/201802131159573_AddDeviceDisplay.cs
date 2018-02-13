namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDeviceDisplay : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.DeviceDisplay", new[] { "DeviceTypeId", "DeviceDatasheetId", "Id" }, "dbo.DeviceBase");
            DropIndex("dbo.DeviceDisplay", new[] { "DeviceTypeId", "DeviceDatasheetId", "Id" });
            DropTable("dbo.DeviceDisplay");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.DeviceDisplay",
                c => new
                    {
                        DeviceTypeId = c.Byte(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        Id = c.Guid(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceTypeId, t.DeviceDatasheetId, t.Id })
                .ForeignKey("dbo.DeviceBase", t => new { t.DeviceTypeId, t.DeviceDatasheetId, t.Id })
                .Index(t => new { t.DeviceTypeId, t.DeviceDatasheetId, t.Id });
        }

        #endregion Methods
    }
}