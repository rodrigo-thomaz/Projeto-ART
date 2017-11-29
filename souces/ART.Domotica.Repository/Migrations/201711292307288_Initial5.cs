namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorUnitOfMeasurementDefault",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        UnitOfMeasurementId = c.Short(nullable: false),
                        UnitOfMeasurementTypeId = c.Byte(nullable: false),
                        Min = c.Decimal(nullable: false, precision: 9, scale: 4),
                        Max = c.Decimal(nullable: false, precision: 9, scale: 4),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorTypeId })
                .ForeignKey("dbo.SensorDatasheet", t => new { t.Id, t.SensorTypeId })
                .ForeignKey("dbo.SensorType", t => t.SensorTypeId)
                .ForeignKey("dbo.UnitOfMeasurement", t => new { t.UnitOfMeasurementId, t.UnitOfMeasurementTypeId })
                .Index(t => new { t.Id, t.SensorTypeId })
                .Index(t => new { t.UnitOfMeasurementId, t.UnitOfMeasurementTypeId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorUnitOfMeasurementDefault", new[] { "UnitOfMeasurementId", "UnitOfMeasurementTypeId" }, "dbo.UnitOfMeasurement");
            DropForeignKey("dbo.SensorUnitOfMeasurementDefault", "SensorTypeId", "dbo.SensorType");
            DropForeignKey("dbo.SensorUnitOfMeasurementDefault", new[] { "Id", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropIndex("dbo.SensorUnitOfMeasurementDefault", new[] { "UnitOfMeasurementId", "UnitOfMeasurementTypeId" });
            DropIndex("dbo.SensorUnitOfMeasurementDefault", new[] { "Id", "SensorTypeId" });
            DropTable("dbo.SensorUnitOfMeasurementDefault");
        }
    }
}
