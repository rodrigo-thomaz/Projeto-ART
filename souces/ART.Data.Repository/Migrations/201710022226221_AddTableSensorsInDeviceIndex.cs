namespace ART.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableSensorsInDeviceIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            CreateIndex("dbo.SensorsInDevice", "SensorBaseId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            CreateIndex("dbo.SensorsInDevice", "SensorBaseId");
        }
    }
}
