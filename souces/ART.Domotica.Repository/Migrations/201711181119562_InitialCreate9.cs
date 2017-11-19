namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate9 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HighAlarmValue", c => c.Decimal(nullable: false, precision: 9, scale: 4));
            AddColumn("dbo.DSFamilyTempSensor", "LowAlarmValue", c => c.Decimal(nullable: false, precision: 9, scale: 4));
            DropColumn("dbo.DSFamilyTempSensor", "HighAlarmCelsius");
            DropColumn("dbo.DSFamilyTempSensor", "LowAlarmCelsius");
        }

        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensor", "LowAlarmCelsius", c => c.Decimal(nullable: false, precision: 9, scale: 4));
            AddColumn("dbo.DSFamilyTempSensor", "HighAlarmCelsius", c => c.Decimal(nullable: false, precision: 9, scale: 4));
            DropColumn("dbo.DSFamilyTempSensor", "LowAlarmValue");
            DropColumn("dbo.DSFamilyTempSensor", "HighAlarmValue");
        }

        #endregion Methods
    }
}