namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sensor", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropIndex("dbo.Sensor", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            DropColumn("dbo.Sensor", "UnitMeasurementId");
            DropColumn("dbo.Sensor", "UnitMeasurementTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensor", "UnitMeasurementTypeId", c => c.Byte(nullable: false));
            AddColumn("dbo.Sensor", "UnitMeasurementId", c => c.Short(nullable: false));
            CreateIndex("dbo.Sensor", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            AddForeignKey("dbo.Sensor", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement", new[] { "Id", "UnitMeasurementTypeId" });
        }
    }
}
