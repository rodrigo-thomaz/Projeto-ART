namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DSFamilyTempSensor", "LowChartLimiterCelsius");
            DropColumn("dbo.DSFamilyTempSensor", "HighChartLimiterCelsius");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HighChartLimiterCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
            AddColumn("dbo.DSFamilyTempSensor", "LowChartLimiterCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
        }
    }
}
