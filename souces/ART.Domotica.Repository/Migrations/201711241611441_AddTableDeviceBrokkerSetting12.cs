namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDeviceBrokkerSetting12 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DeviceBrokerSetting", newName: "DeviceMQ");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DeviceMQ", newName: "DeviceBrokerSetting");
        }
    }
}
