namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DSFamilyTempSensorResolution", newName: "SensorTempDSFamilyResolution");
            DropForeignKey("dbo.DSFamilyTempSensor", "DSFamilyTempSensorResolutionId", "dbo.DSFamilyTempSensorResolution");
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.Sensor");
            DropIndex("dbo.DSFamilyTempSensor", new[] { "Id" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DSFamilyTempSensorResolutionId" });
            CreateTable(
                "dbo.SensorTempDSFamily",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceAddress = c.String(nullable: false, maxLength: 32),
                        Family = c.String(nullable: false, maxLength: 10),
                        SensorTempDSFamilyResolutionId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensor", t => t.Id)
                .ForeignKey("dbo.SensorTempDSFamilyResolution", t => t.SensorTempDSFamilyResolutionId)
                .Index(t => t.Id)
                .Index(t => t.DeviceAddress, unique: true)
                .Index(t => t.SensorTempDSFamilyResolutionId);
            
            DropTable("dbo.DSFamilyTempSensor");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DSFamilyTempSensor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceAddress = c.String(nullable: false, maxLength: 32),
                        Family = c.String(nullable: false, maxLength: 10),
                        DSFamilyTempSensorResolutionId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.SensorTempDSFamily", "SensorTempDSFamilyResolutionId", "dbo.SensorTempDSFamilyResolution");
            DropForeignKey("dbo.SensorTempDSFamily", "Id", "dbo.Sensor");
            DropIndex("dbo.SensorTempDSFamily", new[] { "SensorTempDSFamilyResolutionId" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "DeviceAddress" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "Id" });
            DropTable("dbo.SensorTempDSFamily");
            CreateIndex("dbo.DSFamilyTempSensor", "DSFamilyTempSensorResolutionId");
            CreateIndex("dbo.DSFamilyTempSensor", "DeviceAddress", unique: true);
            CreateIndex("dbo.DSFamilyTempSensor", "Id");
            AddForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.DSFamilyTempSensor", "DSFamilyTempSensorResolutionId", "dbo.DSFamilyTempSensorResolution", "Id");
            RenameTable(name: "dbo.SensorTempDSFamilyResolution", newName: "DSFamilyTempSensorResolution");
        }
    }
}
