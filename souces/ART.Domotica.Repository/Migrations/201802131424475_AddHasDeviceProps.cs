namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddHasDeviceProps : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceDatasheet", "HasDeviceWiFi");
            DropColumn("dbo.DeviceDatasheet", "HasDeviceNTP");
            DropColumn("dbo.DeviceDatasheet", "HasDeviceMQ");
            DropColumn("dbo.DeviceDatasheet", "HasDeviceDisplay");
            DropColumn("dbo.DeviceDatasheet", "HasDeviceDebug");
            DropColumn("dbo.DeviceDatasheet", "HasDeviceBinary");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceDatasheet", "HasDeviceBinary", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDatasheet", "HasDeviceDebug", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDatasheet", "HasDeviceDisplay", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDatasheet", "HasDeviceMQ", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDatasheet", "HasDeviceNTP", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDatasheet", "HasDeviceWiFi", c => c.Boolean(nullable: false));
        }

        #endregion Methods
    }
}