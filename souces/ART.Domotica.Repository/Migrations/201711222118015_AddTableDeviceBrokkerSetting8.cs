namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDeviceBrokkerSetting8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceNTPSetting", "TimeOffsetInSecond", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceNTPSetting", "UpdateIntervalInMilliSecond", c => c.Int(nullable: false));
            DropColumn("dbo.DeviceNTPSetting", "TimeOffsetInSeconds");
            DropColumn("dbo.DeviceNTPSetting", "UpdateIntervalInMilliSeconds");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceNTPSetting", "UpdateIntervalInMilliSeconds", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceNTPSetting", "TimeOffsetInSeconds", c => c.Int(nullable: false));
            DropColumn("dbo.DeviceNTPSetting", "UpdateIntervalInMilliSecond");
            DropColumn("dbo.DeviceNTPSetting", "TimeOffsetInSecond");
        }
    }
}