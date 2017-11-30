namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial9 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SensorBase", newName: "Sensor");
            RenameColumn(table: "dbo.SensorsInDevice", name: "SensorBaseId", newName: "SensorId");
            RenameColumn(table: "dbo.SensorTrigger", name: "SensorBaseId", newName: "SensorId");
            RenameIndex(table: "dbo.SensorTrigger", name: "IX_SensorBaseId", newName: "IX_SensorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SensorTrigger", name: "IX_SensorId", newName: "IX_SensorBaseId");
            RenameColumn(table: "dbo.SensorTrigger", name: "SensorId", newName: "SensorBaseId");
            RenameColumn(table: "dbo.SensorsInDevice", name: "SensorId", newName: "SensorBaseId");
            RenameTable(name: "dbo.Sensor", newName: "SensorBase");
        }
    }
}
