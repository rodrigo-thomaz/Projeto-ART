namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "TempSensorRangeId", newName: "SensorRangeId");
            RenameIndex(table: "dbo.DSFamilyTempSensor", name: "IX_TempSensorRangeId", newName: "IX_SensorRangeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DSFamilyTempSensor", name: "IX_SensorRangeId", newName: "IX_TempSensorRangeId");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "SensorRangeId", newName: "TempSensorRangeId");
        }
    }
}
