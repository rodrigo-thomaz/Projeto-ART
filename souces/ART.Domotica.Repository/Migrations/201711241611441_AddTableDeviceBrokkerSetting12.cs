namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting12 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameTable(name: "dbo.DeviceMQ", newName: "DeviceBrokerSetting");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.DeviceBrokerSetting", newName: "DeviceMQ");
        }

        #endregion Methods
    }
}