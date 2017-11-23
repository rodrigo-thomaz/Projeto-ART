namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting5 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DSFamilyTempSensor", "Label");
        }

        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensor", "Label", c => c.String(nullable: false, maxLength: 50));
        }

        #endregion Methods
    }
}