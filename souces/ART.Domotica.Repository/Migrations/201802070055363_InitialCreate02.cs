namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate02 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AlterColumn("dbo.DeviceWiFi", "PublishIntervalInMilliSeconds", c => c.Int(nullable: false));
            AlterColumn("dbo.DeviceSensors", "PublishIntervalInMilliSeconds", c => c.Int(nullable: false));
            AlterColumn("dbo.DeviceSensors", "ReadIntervalInMilliSeconds", c => c.Int(nullable: false));
            AlterColumn("dbo.DeviceNTP", "UpdateIntervalInMilliSecond", c => c.Int(nullable: false));
        }

        public override void Up()
        {
            AlterColumn("dbo.DeviceNTP", "UpdateIntervalInMilliSecond", c => c.Long(nullable: false));
            AlterColumn("dbo.DeviceSensors", "ReadIntervalInMilliSeconds", c => c.Long(nullable: false));
            AlterColumn("dbo.DeviceSensors", "PublishIntervalInMilliSeconds", c => c.Long(nullable: false));
            AlterColumn("dbo.DeviceWiFi", "PublishIntervalInMilliSeconds", c => c.Long(nullable: false));
        }

        #endregion Methods
    }
}