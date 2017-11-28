namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate76 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DSFamilyTempSensor", new[] { "SensorRangeId" });
            AlterColumn("dbo.DSFamilyTempSensor", "SensorRangeId", c => c.Byte());
            CreateIndex("dbo.DSFamilyTempSensor", "SensorRangeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DSFamilyTempSensor", new[] { "SensorRangeId" });
            AlterColumn("dbo.DSFamilyTempSensor", "SensorRangeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.DSFamilyTempSensor", "SensorRangeId");
        }
    }
}
