namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate07 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.DeviceSerial", new[] { "DeviceId", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropIndex("dbo.DeviceSerial", new[] { "DeviceId", "DeviceDatasheetId" });
        }

        public override void Up()
        {
            CreateIndex("dbo.DeviceSerial", new[] { "DeviceId", "DeviceDatasheetId" });
            AddForeignKey("dbo.DeviceSerial", new[] { "DeviceId", "DeviceDatasheetId" }, "dbo.DeviceBase", new[] { "Id", "DeviceDatasheetId" });
        }

        #endregion Methods
    }
}