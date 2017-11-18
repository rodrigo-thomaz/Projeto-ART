namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DSFamilyTempSensor", "LowAlarmValue", c => c.Decimal(nullable: false, precision: 9, scale: 4));
            AlterColumn("dbo.DSFamilyTempSensor", "HighAlarmValue", c => c.Decimal(nullable: false, precision: 9, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DSFamilyTempSensor", "HighAlarmValue", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AlterColumn("dbo.DSFamilyTempSensor", "LowAlarmValue", c => c.Decimal(nullable: false, precision: 6, scale: 3));
        }
    }
}
