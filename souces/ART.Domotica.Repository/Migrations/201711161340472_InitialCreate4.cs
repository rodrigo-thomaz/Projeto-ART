namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensor", "LowTempSensorAlarmOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "LowTempSensorAlarmValue", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AddColumn("dbo.DSFamilyTempSensor", "LowTempSensorAlarmBuzzerOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "HighTempSensorAlarmOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "HighTempSensorAlarmValue", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AddColumn("dbo.DSFamilyTempSensor", "HighTempSensorAlarmBuzzerOn", c => c.Boolean(nullable: false));
            DropColumn("dbo.DSFamilyTempSensor", "HasAlarm");
            DropColumn("dbo.DSFamilyTempSensor", "LowAlarm");
            DropColumn("dbo.DSFamilyTempSensor", "HighAlarm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HighAlarm", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AddColumn("dbo.DSFamilyTempSensor", "LowAlarm", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AddColumn("dbo.DSFamilyTempSensor", "HasAlarm", c => c.Boolean(nullable: false));
            DropColumn("dbo.DSFamilyTempSensor", "HighTempSensorAlarmBuzzerOn");
            DropColumn("dbo.DSFamilyTempSensor", "HighTempSensorAlarmValue");
            DropColumn("dbo.DSFamilyTempSensor", "HighTempSensorAlarmOn");
            DropColumn("dbo.DSFamilyTempSensor", "LowTempSensorAlarmBuzzerOn");
            DropColumn("dbo.DSFamilyTempSensor", "LowTempSensorAlarmValue");
            DropColumn("dbo.DSFamilyTempSensor", "LowTempSensorAlarmOn");
        }
    }
}
