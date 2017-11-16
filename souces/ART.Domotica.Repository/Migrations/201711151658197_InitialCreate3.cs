namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate3 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HasHighAlarm", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "HasLowAlarm", c => c.Boolean(nullable: false));
            DropColumn("dbo.DSFamilyTempSensor", "HasAlarm");
        }

        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HasAlarm", c => c.Boolean(nullable: false));
            DropColumn("dbo.DSFamilyTempSensor", "HasLowAlarm");
            DropColumn("dbo.DSFamilyTempSensor", "HasHighAlarm");
        }

        #endregion Methods
    }
}