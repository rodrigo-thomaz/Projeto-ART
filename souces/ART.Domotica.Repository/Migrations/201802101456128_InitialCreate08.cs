namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate08 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.DeviceSerial", "IX_Unique_DeviceSerialIndex");
            RenameIndex(table: "dbo.DeviceSerial", name: "FK_DeviceSerial_DeviceBase_DeviceId_DeviceDatasheetId", newName: "IX_DeviceId_DeviceDatasheetId");
        }

        public override void Up()
        {
            RenameIndex(table: "dbo.DeviceSerial", name: "IX_DeviceId_DeviceDatasheetId", newName: "FK_DeviceSerial_DeviceBase_DeviceId_DeviceDatasheetId");
            CreateIndex("dbo.DeviceSerial", new[] { "Id", "DeviceId", "DeviceDatasheetId", "Index" }, unique: true, name: "IX_Unique_DeviceSerialIndex");
        }

        #endregion Methods
    }
}