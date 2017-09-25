namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HardwareBase",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DSFamilyTempSensorResolution",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Resolution = c.Decimal(nullable: false, precision: 5, scale: 4),
                        ConversionTime = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Bits = c.Byte(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Bits, unique: true);
            
            CreateTable(
                "dbo.HardwareInSpace",
                c => new
                    {
                        HardwareBaseId = c.Guid(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.HardwareBaseId, t.SpaceId })
                .ForeignKey("dbo.HardwareBase", t => t.HardwareBaseId)
                .ForeignKey("dbo.Space", t => t.SpaceId)
                .Index(t => t.HardwareBaseId)
                .Index(t => t.SpaceId);
            
            CreateTable(
                "dbo.Space",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.UserInSpace",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SpaceId })
                .ForeignKey("dbo.Space", t => t.SpaceId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SpaceId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TemperatureScale",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.SensorBase",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HardwareBase", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.DSFamilyTempSensor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceAddress = c.String(nullable: false, maxLength: 15),
                        Family = c.String(nullable: false, maxLength: 10),
                        TemperatureScaleId = c.Byte(nullable: false),
                        DSFamilyTempSensorResolutionId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorBase", t => t.Id)
                .ForeignKey("dbo.TemperatureScale", t => t.TemperatureScaleId)
                .ForeignKey("dbo.DSFamilyTempSensorResolution", t => t.DSFamilyTempSensorResolutionId)
                .Index(t => t.Id)
                .Index(t => t.DeviceAddress, unique: true)
                .Index(t => t.TemperatureScaleId)
                .Index(t => t.DSFamilyTempSensorResolutionId);
            
            CreateTable(
                "dbo.DeviceBase",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HardwareBase", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ESPDeviceBase",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.MacAddress, unique: true);
            
            CreateTable(
                "dbo.ThermometerDevice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ESPDeviceBase", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.RaspberryDeviceBase",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LanMacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                        WLanMacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.LanMacAddress, unique: true)
                .Index(t => t.WLanMacAddress, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaspberryDeviceBase", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.ThermometerDevice", "Id", "dbo.ESPDeviceBase");
            DropForeignKey("dbo.ESPDeviceBase", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.DSFamilyTempSensor", "DSFamilyTempSensorResolutionId", "dbo.DSFamilyTempSensorResolution");
            DropForeignKey("dbo.DSFamilyTempSensor", "TemperatureScaleId", "dbo.TemperatureScale");
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwareInSpace", "SpaceId", "dbo.Space");
            DropForeignKey("dbo.UserInSpace", "UserId", "dbo.User");
            DropForeignKey("dbo.UserInSpace", "SpaceId", "dbo.Space");
            DropForeignKey("dbo.HardwareInSpace", "HardwareBaseId", "dbo.HardwareBase");
            DropIndex("dbo.RaspberryDeviceBase", new[] { "WLanMacAddress" });
            DropIndex("dbo.RaspberryDeviceBase", new[] { "LanMacAddress" });
            DropIndex("dbo.RaspberryDeviceBase", new[] { "Id" });
            DropIndex("dbo.ThermometerDevice", new[] { "Id" });
            DropIndex("dbo.ESPDeviceBase", new[] { "MacAddress" });
            DropIndex("dbo.ESPDeviceBase", new[] { "Id" });
            DropIndex("dbo.DeviceBase", new[] { "Id" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DSFamilyTempSensorResolutionId" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "TemperatureScaleId" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "Id" });
            DropIndex("dbo.SensorBase", new[] { "Id" });
            DropIndex("dbo.TemperatureScale", new[] { "Name" });
            DropIndex("dbo.UserInSpace", new[] { "SpaceId" });
            DropIndex("dbo.UserInSpace", new[] { "UserId" });
            DropIndex("dbo.Space", new[] { "Name" });
            DropIndex("dbo.HardwareInSpace", new[] { "SpaceId" });
            DropIndex("dbo.HardwareInSpace", new[] { "HardwareBaseId" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Bits" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Name" });
            DropTable("dbo.RaspberryDeviceBase");
            DropTable("dbo.ThermometerDevice");
            DropTable("dbo.ESPDeviceBase");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.DSFamilyTempSensor");
            DropTable("dbo.SensorBase");
            DropTable("dbo.TemperatureScale");
            DropTable("dbo.User");
            DropTable("dbo.UserInSpace");
            DropTable("dbo.Space");
            DropTable("dbo.HardwareInSpace");
            DropTable("dbo.DSFamilyTempSensorResolution");
            DropTable("dbo.HardwareBase");
        }
    }
}
