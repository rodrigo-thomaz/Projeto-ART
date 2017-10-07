namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.HardwaresInApplication",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ApplicationId = c.Guid(nullable: false),
                        HardwareBaseId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId)
                .ForeignKey("dbo.HardwareBase", t => t.HardwareBaseId)
                .Index(t => new { t.ApplicationId, t.HardwareBaseId }, unique: true, name: "IX_Unique_ApplicationId_HardwareBaseId")
                .Index(t => t.HardwareBaseId, unique: true, name: "IX_Unique_HardwareBaseId")
                .Index(t => t.CreateByApplicationUserId);
            
            CreateTable(
                "dbo.HardwareBase",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SensorsInDevice",
                c => new
                    {
                        SensorBaseId = c.Guid(nullable: false),
                        DeviceBaseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SensorBaseId, t.DeviceBaseId })
                .ForeignKey("dbo.DeviceBase", t => t.DeviceBaseId)
                .ForeignKey("dbo.SensorBase", t => t.SensorBaseId)
                .Index(t => t.SensorBaseId, unique: true)
                .Index(t => t.DeviceBaseId);
            
            CreateTable(
                "dbo.DSFamilyTempSensorResolution",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Bits = c.Byte(nullable: false),
                        Resolution = c.Decimal(nullable: false, precision: 5, scale: 4),
                        ConversionTime = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Bits, unique: true);
            
            CreateTable(
                "dbo.TemperatureScale",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Symbol = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Symbol, unique: true);
            
            CreateTable(
                "dbo.HardwaresInProject",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        HardwaresInApplicationId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.HardwaresInApplication", t => t.HardwaresInApplicationId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => new { t.HardwaresInApplicationId, t.ProjectId }, unique: true, name: "IX_Unique_HardwaresInApplicationId_ProjectId")
                .Index(t => t.CreateByApplicationUserId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        ApplicationId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId)
                .Index(t => t.ApplicationId)
                .Index(t => t.CreateByApplicationUserId);
            
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
                        HighAlarm = c.Decimal(nullable: false, precision: 6, scale: 3),
                        LowAlarm = c.Decimal(nullable: false, precision: 6, scale: 3),
                        TemperatureScaleId = c.Byte(nullable: false),
                        DSFamilyTempSensorResolutionId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorBase", t => t.Id)
                .ForeignKey("dbo.DSFamilyTempSensorResolution", t => t.DSFamilyTempSensorResolutionId)
                .ForeignKey("dbo.TemperatureScale", t => t.TemperatureScaleId)
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
            DropForeignKey("dbo.DSFamilyTempSensor", "TemperatureScaleId", "dbo.TemperatureScale");
            DropForeignKey("dbo.DSFamilyTempSensor", "DSFamilyTempSensorResolutionId", "dbo.DSFamilyTempSensorResolution");
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwaresInProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Project", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.HardwaresInProject", "HardwaresInApplicationId", "dbo.HardwaresInApplication");
            DropForeignKey("dbo.HardwaresInProject", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.HardwaresInApplication", "HardwareBaseId", "dbo.HardwareBase");
            DropForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.SensorsInDevice", "DeviceBaseId", "dbo.DeviceBase");
            DropForeignKey("dbo.HardwaresInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.HardwaresInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationUser", "ApplicationId", "dbo.Application");
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
            DropIndex("dbo.Project", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.Project", new[] { "ApplicationId" });
            DropIndex("dbo.HardwaresInProject", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.HardwaresInProject", "IX_Unique_HardwaresInApplicationId_ProjectId");
            DropIndex("dbo.TemperatureScale", new[] { "Symbol" });
            DropIndex("dbo.TemperatureScale", new[] { "Name" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Bits" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Name" });
            DropIndex("dbo.SensorsInDevice", new[] { "DeviceBaseId" });
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            DropIndex("dbo.HardwaresInApplication", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.HardwaresInApplication", "IX_Unique_HardwareBaseId");
            DropIndex("dbo.HardwaresInApplication", "IX_Unique_ApplicationId_HardwareBaseId");
            DropIndex("dbo.ApplicationUser", new[] { "ApplicationId" });
            DropTable("dbo.RaspberryDeviceBase");
            DropTable("dbo.ThermometerDevice");
            DropTable("dbo.ESPDeviceBase");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.DSFamilyTempSensor");
            DropTable("dbo.SensorBase");
            DropTable("dbo.Project");
            DropTable("dbo.HardwaresInProject");
            DropTable("dbo.TemperatureScale");
            DropTable("dbo.DSFamilyTempSensorResolution");
            DropTable("dbo.SensorsInDevice");
            DropTable("dbo.HardwareBase");
            DropTable("dbo.HardwaresInApplication");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Application");
        }
    }
}
