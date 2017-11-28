namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DSFamilyTempSensor", "LowAlarmOn");
            DropColumn("dbo.DSFamilyTempSensor", "LowAlarmCelsius");
            DropColumn("dbo.DSFamilyTempSensor", "LowAlarmBuzzerOn");
            DropColumn("dbo.DSFamilyTempSensor", "HighAlarmOn");
            DropColumn("dbo.DSFamilyTempSensor", "HighAlarmCelsius");
            DropColumn("dbo.DSFamilyTempSensor", "HighAlarmBuzzerOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HighAlarmBuzzerOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "HighAlarmCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
            AddColumn("dbo.DSFamilyTempSensor", "HighAlarmOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "LowAlarmBuzzerOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "LowAlarmCelsius", c => c.Decimal(nullable: false, precision: 7, scale: 4));
            AddColumn("dbo.DSFamilyTempSensor", "LowAlarmOn", c => c.Boolean(nullable: false));
        }
    }
}
