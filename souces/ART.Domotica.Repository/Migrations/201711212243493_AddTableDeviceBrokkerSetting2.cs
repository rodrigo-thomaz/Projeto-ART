namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.DeviceBrokerSetting", new[] { "ClientId" });
            AlterColumn("dbo.DeviceBrokerSetting", "ClientId", c => c.String(nullable: false, maxLength: 4));
            CreateIndex("dbo.DeviceBrokerSetting", "ClientId", unique: true);
        }

        public override void Up()
        {
            DropIndex("dbo.DeviceBrokerSetting", new[] { "ClientId" });
            AlterColumn("dbo.DeviceBrokerSetting", "ClientId", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.DeviceBrokerSetting", "ClientId", unique: true);
        }

        #endregion Methods
    }
}