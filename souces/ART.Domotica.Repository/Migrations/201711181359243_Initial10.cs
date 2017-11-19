namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial10 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AlterColumn("dbo.DSFamilyTempSensor", "HighAlarmCelsius", c => c.Decimal(nullable: false, precision: 9, scale: 4));
            AlterColumn("dbo.DSFamilyTempSensor", "LowAlarmCelsius", c => c.Decimal(nullable: false, precision: 9, scale: 4));
        }

        public override void Up()
        {
            AlterColumn("dbo.DSFamilyTempSensor", "LowAlarmCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
            AlterColumn("dbo.DSFamilyTempSensor", "HighAlarmCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
        }

        #endregion Methods
    }
}