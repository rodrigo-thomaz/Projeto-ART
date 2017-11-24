namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting13 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameTable(name: "dbo.DeviceNTP", newName: "DeviceNTPSetting");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.DeviceNTPSetting", newName: "DeviceNTP");
        }

        #endregion Methods
    }
}