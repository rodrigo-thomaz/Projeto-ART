namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DSFamilyTempSensor", "HighChartLimiterCelsius");
            DropColumn("dbo.DSFamilyTempSensor", "LowChartLimiterCelsius");
        }

        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensor", "LowChartLimiterCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
            AddColumn("dbo.DSFamilyTempSensor", "HighChartLimiterCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
        }

        #endregion Methods
    }
}