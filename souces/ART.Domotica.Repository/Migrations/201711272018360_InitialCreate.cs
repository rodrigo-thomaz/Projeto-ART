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
                "dbo.ApplicationMQ",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        User = c.String(nullable: false, maxLength: 12),
                        Password = c.String(nullable: false, maxLength: 12),
                        Topic = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Topic, unique: true);
            
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
                "dbo.DeviceInApplication",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ApplicationId = c.Guid(nullable: false),
                        DeviceBaseId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId)
                .ForeignKey("dbo.DeviceBase", t => t.DeviceBaseId)
                .Index(t => new { t.ApplicationId, t.DeviceBaseId }, unique: true, name: "IX_Unique_ApplicationId_DeviceBaseId")
                .Index(t => t.DeviceBaseId, unique: true, name: "IX_Unique_DeviceBaseId")
                .Index(t => t.CreateByApplicationUserId);
            
            CreateTable(
                "dbo.HardwareBase",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Label = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceMQ",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        User = c.String(nullable: false, maxLength: 12),
                        Password = c.String(nullable: false, maxLength: 12),
                        ClientId = c.String(nullable: false, maxLength: 32),
                        Topic = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.User, unique: true)
                .Index(t => t.Password, unique: true)
                .Index(t => t.ClientId, unique: true)
                .Index(t => t.Topic, unique: true);
            
            CreateTable(
                "dbo.DeviceNTP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeZoneId = c.Byte(nullable: false),
                        UpdateIntervalInMilliSecond = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .ForeignKey("dbo.TimeZone", t => t.TimeZoneId)
                .Index(t => t.Id)
                .Index(t => t.TimeZoneId);
            
            CreateTable(
                "dbo.TimeZone",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false, maxLength: 100),
                        SupportsDaylightSavingTime = c.Boolean(nullable: false),
                        UtcTimeOffsetInSecond = c.Int(nullable: false),
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
                "dbo.UnitOfMeasurement",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        UnitOfMeasurementTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Symbol = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.UnitOfMeasurementTypeId })
                .ForeignKey("dbo.UnitOfMeasurementType", t => t.UnitOfMeasurementTypeId)
                .Index(t => t.UnitOfMeasurementTypeId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Symbol, unique: true);
            
            CreateTable(
                "dbo.UnitOfMeasurementType",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.DSFamilyTempSensorResolution",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Bits = c.Byte(nullable: false),
                        Resolution = c.Decimal(nullable: false, precision: 5, scale: 4),
                        DecimalPlaces = c.Byte(nullable: false),
                        ConversionTime = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Bits, unique: true);
            
            CreateTable(
                "dbo.TempSensorRange",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Min = c.Short(nullable: false),
                        Max = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HardwaresInProject",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DeviceInApplicationId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceInApplication", t => t.DeviceInApplicationId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => new { t.DeviceInApplicationId, t.ProjectId }, unique: true, name: "IX_Unique_DeviceInApplicationId_ProjectId")
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
                        UnitOfMeasurementId = c.Byte(nullable: false),
                        UnitOfMeasurementTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HardwareBase", t => t.Id)
                .ForeignKey("dbo.UnitOfMeasurement", t => new { t.UnitOfMeasurementId, t.UnitOfMeasurementTypeId })
                .Index(t => t.Id)
                .Index(t => new { t.UnitOfMeasurementId, t.UnitOfMeasurementTypeId });
            
            CreateTable(
                "dbo.DSFamilyTempSensor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceAddress = c.String(nullable: false, maxLength: 32),
                        Family = c.String(nullable: false, maxLength: 10),
                        TempSensorRangeId = c.Byte(nullable: false),
                        DSFamilyTempSensorResolutionId = c.Byte(nullable: false),
                        LowAlarmOn = c.Boolean(nullable: false),
                        LowAlarmCelsius = c.Decimal(nullable: false, precision: 7, scale: 4),
                        LowAlarmBuzzerOn = c.Boolean(nullable: false),
                        HighAlarmOn = c.Boolean(nullable: false),
                        HighAlarmCelsius = c.Decimal(nullable: false, precision: 7, scale: 4),
                        HighAlarmBuzzerOn = c.Boolean(nullable: false),
                        LowChartLimiterCelsius = c.Decimal(nullable: false, precision: 7, scale: 4),
                        HighChartLimiterCelsius = c.Decimal(nullable: false, precision: 7, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorBase", t => t.Id)
                .ForeignKey("dbo.DSFamilyTempSensorResolution", t => t.DSFamilyTempSensorResolutionId)
                .ForeignKey("dbo.TempSensorRange", t => t.TempSensorRangeId)
                .Index(t => t.Id)
                .Index(t => t.DeviceAddress, unique: true)
                .Index(t => t.TempSensorRangeId)
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
                "dbo.ESPDevice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChipId = c.Int(nullable: false),
                        FlashChipId = c.Int(nullable: false),
                        MacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                        Pin = c.String(nullable: false, maxLength: 4, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.ChipId)
                .Index(t => t.FlashChipId, unique: true)
                .Index(t => t.MacAddress, unique: true)
                .Index(t => t.Pin, unique: true);
            
            CreateTable(
                "dbo.RaspberryDevice",
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
            DropForeignKey("dbo.RaspberryDevice", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.ESPDevice", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.DSFamilyTempSensor", "TempSensorRangeId", "dbo.TempSensorRange");
            DropForeignKey("dbo.DSFamilyTempSensor", "DSFamilyTempSensorResolutionId", "dbo.DSFamilyTempSensorResolution");
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorBase", new[] { "UnitOfMeasurementId", "UnitOfMeasurementTypeId" }, "dbo.UnitOfMeasurement");
            DropForeignKey("dbo.SensorBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwaresInProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Project", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.HardwaresInProject", "DeviceInApplicationId", "dbo.DeviceInApplication");
            DropForeignKey("dbo.HardwaresInProject", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.DeviceInApplication", "DeviceBaseId", "dbo.DeviceBase");
            DropForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.UnitOfMeasurement", "UnitOfMeasurementTypeId", "dbo.UnitOfMeasurementType");
            DropForeignKey("dbo.SensorsInDevice", "DeviceBaseId", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceNTP", "TimeZoneId", "dbo.TimeZone");
            DropForeignKey("dbo.DeviceNTP", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceMQ", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.DeviceInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationUser", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationMQ", "Id", "dbo.Application");
            DropIndex("dbo.RaspberryDevice", new[] { "WLanMacAddress" });
            DropIndex("dbo.RaspberryDevice", new[] { "LanMacAddress" });
            DropIndex("dbo.RaspberryDevice", new[] { "Id" });
            DropIndex("dbo.ESPDevice", new[] { "Pin" });
            DropIndex("dbo.ESPDevice", new[] { "MacAddress" });
            DropIndex("dbo.ESPDevice", new[] { "FlashChipId" });
            DropIndex("dbo.ESPDevice", new[] { "ChipId" });
            DropIndex("dbo.ESPDevice", new[] { "Id" });
            DropIndex("dbo.DeviceBase", new[] { "Id" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DSFamilyTempSensorResolutionId" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "TempSensorRangeId" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "Id" });
            DropIndex("dbo.SensorBase", new[] { "UnitOfMeasurementId", "UnitOfMeasurementTypeId" });
            DropIndex("dbo.SensorBase", new[] { "Id" });
            DropIndex("dbo.Project", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.Project", new[] { "ApplicationId" });
            DropIndex("dbo.HardwaresInProject", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.HardwaresInProject", "IX_Unique_DeviceInApplicationId_ProjectId");
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Bits" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Name" });
            DropIndex("dbo.UnitOfMeasurementType", new[] { "Name" });
            DropIndex("dbo.UnitOfMeasurement", new[] { "Symbol" });
            DropIndex("dbo.UnitOfMeasurement", new[] { "Name" });
            DropIndex("dbo.UnitOfMeasurement", new[] { "UnitOfMeasurementTypeId" });
            DropIndex("dbo.SensorsInDevice", new[] { "DeviceBaseId" });
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            DropIndex("dbo.DeviceNTP", new[] { "TimeZoneId" });
            DropIndex("dbo.DeviceNTP", new[] { "Id" });
            DropIndex("dbo.DeviceMQ", new[] { "Topic" });
            DropIndex("dbo.DeviceMQ", new[] { "ClientId" });
            DropIndex("dbo.DeviceMQ", new[] { "Password" });
            DropIndex("dbo.DeviceMQ", new[] { "User" });
            DropIndex("dbo.DeviceMQ", new[] { "Id" });
            DropIndex("dbo.DeviceInApplication", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.DeviceInApplication", "IX_Unique_DeviceBaseId");
            DropIndex("dbo.DeviceInApplication", "IX_Unique_ApplicationId_DeviceBaseId");
            DropIndex("dbo.ApplicationUser", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationMQ", new[] { "Topic" });
            DropIndex("dbo.ApplicationMQ", new[] { "Id" });
            DropTable("dbo.RaspberryDevice");
            DropTable("dbo.ESPDevice");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.DSFamilyTempSensor");
            DropTable("dbo.SensorBase");
            DropTable("dbo.Project");
            DropTable("dbo.HardwaresInProject");
            DropTable("dbo.TempSensorRange");
            DropTable("dbo.DSFamilyTempSensorResolution");
            DropTable("dbo.UnitOfMeasurementType");
            DropTable("dbo.UnitOfMeasurement");
            DropTable("dbo.SensorsInDevice");
            DropTable("dbo.TimeZone");
            DropTable("dbo.DeviceNTP");
            DropTable("dbo.DeviceMQ");
            DropTable("dbo.HardwareBase");
            DropTable("dbo.DeviceInApplication");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.ApplicationMQ");
            DropTable("dbo.Application");
        }
    }
}
