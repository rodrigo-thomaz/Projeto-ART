namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameDeviceSernsors : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DeviceDatasheet", "HasDeviceSensors", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDatasheet", "HasDeviceSensor");
            RenameIndex(table: "dbo.SensorInDevice", name: "IX_DeviceTypeId_DeviceDatasheetId_DeviceSensorId", newName: "IX_DeviceTypeId_DeviceDatasheetId_DeviceSensorsId");
            RenameColumn(table: "dbo.SensorInDevice", name: "DeviceSensorId", newName: "DeviceSensorsId");
            RenameTable(name: "dbo.DeviceSensor", newName: "DeviceSensors");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.DeviceSensors", newName: "DeviceSensor");
            RenameColumn(table: "dbo.SensorInDevice", name: "DeviceSensorsId", newName: "DeviceSensorId");
            RenameIndex(table: "dbo.SensorInDevice", name: "IX_DeviceTypeId_DeviceDatasheetId_DeviceSensorsId", newName: "IX_DeviceTypeId_DeviceDatasheetId_DeviceSensorId");
            AddColumn("dbo.DeviceDatasheet", "HasDeviceSensor", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDatasheet", "HasDeviceSensors");
        }

        #endregion Methods
    }
}