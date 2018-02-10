namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate10 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceSerial", "AllowPinSwapTX");
            DropColumn("dbo.DeviceSerial", "AllowPinSwapRX");
            DropColumn("dbo.DeviceSerial", "PinTX");
            DropColumn("dbo.DeviceSerial", "PinRX");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceSerial", "PinRX", c => c.Int());
            AddColumn("dbo.DeviceSerial", "PinTX", c => c.Int());
            AddColumn("dbo.DeviceSerial", "AllowPinSwapRX", c => c.Boolean());
            AddColumn("dbo.DeviceSerial", "AllowPinSwapTX", c => c.Boolean());
        }

        #endregion Methods
    }
}