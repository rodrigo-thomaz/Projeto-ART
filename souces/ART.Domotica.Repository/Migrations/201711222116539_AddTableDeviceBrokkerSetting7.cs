namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting7 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DeviceNTPSetting", "UpdateInterval", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceNTPSetting", "TimeOffset", c => c.Int(nullable: false));
            AddColumn("dbo.ESPDevice", "TimeOffset", c => c.Int(nullable: false));
            DropColumn("dbo.DeviceNTPSetting", "UpdateIntervalInMilliSeconds");
            DropColumn("dbo.DeviceNTPSetting", "TimeOffsetInSeconds");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceNTPSetting", "TimeOffsetInSeconds", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceNTPSetting", "UpdateIntervalInMilliSeconds", c => c.Int(nullable: false));
            DropColumn("dbo.ESPDevice", "TimeOffset");
            DropColumn("dbo.DeviceNTPSetting", "TimeOffset");
            DropColumn("dbo.DeviceNTPSetting", "UpdateInterval");
        }

        #endregion Methods
    }
}