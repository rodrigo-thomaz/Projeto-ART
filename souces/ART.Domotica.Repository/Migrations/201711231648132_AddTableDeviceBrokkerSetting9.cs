namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting9 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameTable(name: "dbo.ApplicationMQ", newName: "ApplicationBrokerSetting");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationBrokerSetting", newName: "ApplicationMQ");
        }

        #endregion Methods
    }
}