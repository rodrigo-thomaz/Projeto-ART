namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameDeviceSernsorId : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameIndex(table: "dbo.SensorInDevice", name: "IX_DeviceTypeId_DeviceDatasheetId_DeviceId", newName: "IX_DeviceTypeId_DeviceDatasheetId_DeviceSensorId");
            RenameColumn(table: "dbo.SensorInDevice", name: "DeviceId", newName: "DeviceSensorId");
        }

        public override void Up()
        {
            RenameColumn(table: "dbo.SensorInDevice", name: "DeviceSensorId", newName: "DeviceId");
            RenameIndex(table: "dbo.SensorInDevice", name: "IX_DeviceTypeId_DeviceDatasheetId_DeviceSensorId", newName: "IX_DeviceTypeId_DeviceDatasheetId_DeviceId");
        }

        #endregion Methods
    }
}