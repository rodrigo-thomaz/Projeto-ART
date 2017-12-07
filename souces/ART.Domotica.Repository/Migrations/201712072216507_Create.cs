namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActuatorType",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
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
                "dbo.HardwareInApplication",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        HardwareId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.HardwareId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId)
                .ForeignKey("dbo.HardwareBase", t => t.HardwareId)
                .Index(t => t.ApplicationId)
                .Index(t => t.HardwareId, unique: true, name: "IX_Unique_HardwareId")
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
                .ForeignKey("Globalization.TimeZone", t => t.TimeZoneId)
                .Index(t => t.Id)
                .Index(t => t.TimeZoneId);
            
            CreateTable(
                "Globalization.TimeZone",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false, maxLength: 100),
                        SupportsDaylightSavingTime = c.Boolean(nullable: false),
                        UtcTimeOffsetInSecond = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceSensors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublishIntervalInSeconds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SensorInDevice",
                c => new
                    {
                        DeviceSensorsId = c.Guid(nullable: false),
                        SensorId = c.Guid(nullable: false),
                        Ordination = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceSensorsId, t.SensorId })
                .ForeignKey("dbo.DeviceSensors", t => t.DeviceSensorsId)
                .ForeignKey("dbo.Sensor", t => t.SensorId)
                .Index(t => t.DeviceSensorsId)
                .Index(t => t.SensorId, unique: true);
            
            CreateTable(
                "dbo.SensorDatasheet",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorTypeId })
                .ForeignKey("dbo.SensorType", t => t.SensorTypeId)
                .Index(t => t.SensorTypeId);
            
            CreateTable(
                "dbo.SensorDatasheetUnitMeasurementDefault",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        Min = c.Decimal(nullable: false, precision: 9, scale: 4),
                        Max = c.Decimal(nullable: false, precision: 9, scale: 4),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorTypeId })
                .ForeignKey("dbo.SensorDatasheet", t => new { t.Id, t.SensorTypeId })
                .ForeignKey("SI.UnitMeasurementScale", t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId })
                .Index(t => new { t.Id, t.SensorTypeId })
                .Index(t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId });
            
            CreateTable(
                "SI.UnitMeasurementScale",
                c => new
                    {
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId })
                .ForeignKey("SI.NumericalScale", t => new { t.NumericalScaleTypeId, t.NumericalScalePrefixId })
                .ForeignKey("SI.UnitMeasurement", t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId })
                .Index(t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId })
                .Index(t => new { t.NumericalScaleTypeId, t.NumericalScalePrefixId });
            
            CreateTable(
                "SI.NumericalScale",
                c => new
                    {
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        ScientificNotationBase = c.Decimal(nullable: false, precision: 24, scale: 12),
                        ScientificNotationExponent = c.Decimal(nullable: false, precision: 24, scale: 12),
                    })
                .PrimaryKey(t => new { t.NumericalScaleTypeId, t.NumericalScalePrefixId })
                .ForeignKey("SI.NumericalScalePrefix", t => t.NumericalScalePrefixId)
                .ForeignKey("SI.NumericalScaleType", t => t.NumericalScaleTypeId)
                .Index(t => t.NumericalScaleTypeId)
                .Index(t => t.NumericalScalePrefixId);
            
            CreateTable(
                "SI.NumericalScalePrefix",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 15),
                        Symbol = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "SI.NumericalScaleType",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "SI.NumericalScaleTypeCountry",
                c => new
                    {
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        CountryId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.NumericalScaleTypeId, t.CountryId })
                .ForeignKey("Locale.Country", t => t.CountryId)
                .ForeignKey("SI.NumericalScaleType", t => t.NumericalScaleTypeId)
                .Index(t => t.NumericalScaleTypeId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "Locale.Country",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ContinentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Locale.Continent", t => t.ContinentId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ContinentId);
            
            CreateTable(
                "Locale.Continent",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.SensorDatasheetUnitMeasurementScale",
                c => new
                    {
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SensorDatasheetId, t.SensorTypeId, t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId })
                .ForeignKey("dbo.SensorDatasheet", t => new { t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("SI.UnitMeasurementScale", t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId })
                .Index(t => new { t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId });
            
            CreateTable(
                "dbo.SensorUnitMeasurementScale",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        RangeMax = c.Decimal(nullable: false, precision: 7, scale: 4),
                        RangeMin = c.Decimal(nullable: false, precision: 7, scale: 4),
                        ChartLimiterMax = c.Decimal(nullable: false, precision: 7, scale: 4),
                        ChartLimiterMin = c.Decimal(nullable: false, precision: 7, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensor", t => t.Id)
                .ForeignKey("SI.UnitMeasurementScale", t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId })
                .Index(t => t.Id)
                .Index(t => new { t.UnitMeasurementTypeId, t.UnitMeasurementId, t.NumericalScaleTypeId, t.NumericalScalePrefixId });
            
            CreateTable(
                "SI.UnitMeasurement",
                c => new
                    {
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        Id = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Symbol = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.UnitMeasurementTypeId, t.Id })
                .ForeignKey("SI.UnitMeasurementType", t => t.UnitMeasurementTypeId)
                .Index(t => t.UnitMeasurementTypeId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Symbol, unique: true);
            
            CreateTable(
                "SI.UnitMeasurementType",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.SensorType",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
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
            
            CreateTable(
                "dbo.SensorTempDSFamilyResolution",
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
                "dbo.SensorTrigger",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SensorId = c.Guid(nullable: false),
                        TriggerOn = c.Boolean(nullable: false),
                        BuzzerOn = c.Boolean(nullable: false),
                        TriggerValue = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensor", t => t.SensorId)
                .Index(t => t.SensorId);
            
            CreateTable(
                "dbo.HardwaresInProject",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ApplicationId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.HardwareInApplication", t => new { t.ApplicationId, t.DeviceId })
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => new { t.ApplicationId, t.DeviceId, t.ProjectId }, unique: true, name: "IX_Unique_HardwareInApplication_ProjectId")
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
                "dbo.Sensor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HardwareBase", t => t.Id)
                .ForeignKey("dbo.SensorDatasheet", t => new { t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => t.Id)
                .Index(t => new { t.SensorDatasheetId, t.SensorTypeId });
            
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
            DropForeignKey("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("dbo.Sensor", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.ESPDevice", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwaresInProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Project", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.HardwaresInProject", new[] { "ApplicationId", "DeviceId" }, "dbo.HardwareInApplication");
            DropForeignKey("dbo.HardwaresInProject", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.HardwareInApplication", "HardwareId", "dbo.HardwareBase");
            DropForeignKey("dbo.SensorInDevice", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorTrigger", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorTempDSFamily", "SensorTempDSFamilyResolutionId", "dbo.SensorTempDSFamilyResolution");
            DropForeignKey("dbo.SensorTempDSFamily", "Id", "dbo.Sensor");
            DropForeignKey("dbo.SensorDatasheet", "SensorTypeId", "dbo.SensorType");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "UnitMeasurementTypeId", "UnitMeasurementId", "NumericalScaleTypeId", "NumericalScalePrefixId" }, "SI.UnitMeasurementScale");
            DropForeignKey("SI.UnitMeasurementScale", new[] { "UnitMeasurementTypeId", "UnitMeasurementId" }, "SI.UnitMeasurement");
            DropForeignKey("SI.UnitMeasurement", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementTypeId", "UnitMeasurementId", "NumericalScaleTypeId", "NumericalScalePrefixId" }, "SI.UnitMeasurementScale");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "Id", "dbo.Sensor");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementTypeId", "UnitMeasurementId", "NumericalScaleTypeId", "NumericalScalePrefixId" }, "SI.UnitMeasurementScale");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("SI.UnitMeasurementScale", new[] { "NumericalScaleTypeId", "NumericalScalePrefixId" }, "SI.NumericalScale");
            DropForeignKey("SI.NumericalScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("SI.NumericalScaleTypeCountry", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("SI.NumericalScaleTypeCountry", "CountryId", "Locale.Country");
            DropForeignKey("Locale.Country", "ContinentId", "Locale.Continent");
            DropForeignKey("SI.NumericalScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "Id", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("dbo.SensorInDevice", "DeviceSensorsId", "dbo.DeviceSensors");
            DropForeignKey("dbo.DeviceSensors", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceNTP", "TimeZoneId", "Globalization.TimeZone");
            DropForeignKey("dbo.DeviceNTP", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceMQ", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.HardwareInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.HardwareInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationUser", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationMQ", "Id", "dbo.Application");
            DropIndex("dbo.RaspberryDevice", new[] { "WLanMacAddress" });
            DropIndex("dbo.RaspberryDevice", new[] { "LanMacAddress" });
            DropIndex("dbo.RaspberryDevice", new[] { "Id" });
            DropIndex("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" });
            DropIndex("dbo.Sensor", new[] { "Id" });
            DropIndex("dbo.ESPDevice", new[] { "Pin" });
            DropIndex("dbo.ESPDevice", new[] { "MacAddress" });
            DropIndex("dbo.ESPDevice", new[] { "FlashChipId" });
            DropIndex("dbo.ESPDevice", new[] { "ChipId" });
            DropIndex("dbo.ESPDevice", new[] { "Id" });
            DropIndex("dbo.DeviceBase", new[] { "Id" });
            DropIndex("dbo.Project", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.Project", new[] { "ApplicationId" });
            DropIndex("dbo.HardwaresInProject", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.HardwaresInProject", "IX_Unique_HardwareInApplication_ProjectId");
            DropIndex("dbo.SensorTrigger", new[] { "SensorId" });
            DropIndex("dbo.SensorTempDSFamilyResolution", new[] { "Bits" });
            DropIndex("dbo.SensorTempDSFamilyResolution", new[] { "Name" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "SensorTempDSFamilyResolutionId" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "DeviceAddress" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "Id" });
            DropIndex("dbo.SensorType", new[] { "Name" });
            DropIndex("SI.UnitMeasurementType", new[] { "Name" });
            DropIndex("SI.UnitMeasurement", new[] { "Symbol" });
            DropIndex("SI.UnitMeasurement", new[] { "Name" });
            DropIndex("SI.UnitMeasurement", new[] { "UnitMeasurementTypeId" });
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementTypeId", "UnitMeasurementId", "NumericalScaleTypeId", "NumericalScalePrefixId" });
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "Id" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementTypeId", "UnitMeasurementId", "NumericalScaleTypeId", "NumericalScalePrefixId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" });
            DropIndex("Locale.Continent", new[] { "Name" });
            DropIndex("Locale.Country", new[] { "ContinentId" });
            DropIndex("Locale.Country", new[] { "Name" });
            DropIndex("SI.NumericalScaleTypeCountry", new[] { "CountryId" });
            DropIndex("SI.NumericalScaleTypeCountry", new[] { "NumericalScaleTypeId" });
            DropIndex("SI.NumericalScaleType", new[] { "Name" });
            DropIndex("SI.NumericalScalePrefix", new[] { "Name" });
            DropIndex("SI.NumericalScale", new[] { "NumericalScalePrefixId" });
            DropIndex("SI.NumericalScale", new[] { "NumericalScaleTypeId" });
            DropIndex("SI.UnitMeasurementScale", new[] { "NumericalScaleTypeId", "NumericalScalePrefixId" });
            DropIndex("SI.UnitMeasurementScale", new[] { "UnitMeasurementTypeId", "UnitMeasurementId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "UnitMeasurementTypeId", "UnitMeasurementId", "NumericalScaleTypeId", "NumericalScalePrefixId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "Id", "SensorTypeId" });
            DropIndex("dbo.SensorDatasheet", new[] { "SensorTypeId" });
            DropIndex("dbo.SensorInDevice", new[] { "SensorId" });
            DropIndex("dbo.SensorInDevice", new[] { "DeviceSensorsId" });
            DropIndex("dbo.DeviceSensors", new[] { "Id" });
            DropIndex("dbo.DeviceNTP", new[] { "TimeZoneId" });
            DropIndex("dbo.DeviceNTP", new[] { "Id" });
            DropIndex("dbo.DeviceMQ", new[] { "Topic" });
            DropIndex("dbo.DeviceMQ", new[] { "ClientId" });
            DropIndex("dbo.DeviceMQ", new[] { "Password" });
            DropIndex("dbo.DeviceMQ", new[] { "User" });
            DropIndex("dbo.DeviceMQ", new[] { "Id" });
            DropIndex("dbo.HardwareInApplication", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.HardwareInApplication", "IX_Unique_HardwareId");
            DropIndex("dbo.HardwareInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationUser", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationMQ", new[] { "Topic" });
            DropIndex("dbo.ApplicationMQ", new[] { "Id" });
            DropIndex("dbo.ActuatorType", new[] { "Name" });
            DropTable("dbo.RaspberryDevice");
            DropTable("dbo.Sensor");
            DropTable("dbo.ESPDevice");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.Project");
            DropTable("dbo.HardwaresInProject");
            DropTable("dbo.SensorTrigger");
            DropTable("dbo.SensorTempDSFamilyResolution");
            DropTable("dbo.SensorTempDSFamily");
            DropTable("dbo.SensorType");
            DropTable("SI.UnitMeasurementType");
            DropTable("SI.UnitMeasurement");
            DropTable("dbo.SensorUnitMeasurementScale");
            DropTable("dbo.SensorDatasheetUnitMeasurementScale");
            DropTable("Locale.Continent");
            DropTable("Locale.Country");
            DropTable("SI.NumericalScaleTypeCountry");
            DropTable("SI.NumericalScaleType");
            DropTable("SI.NumericalScalePrefix");
            DropTable("SI.NumericalScale");
            DropTable("SI.UnitMeasurementScale");
            DropTable("dbo.SensorDatasheetUnitMeasurementDefault");
            DropTable("dbo.SensorDatasheet");
            DropTable("dbo.SensorInDevice");
            DropTable("dbo.DeviceSensors");
            DropTable("Globalization.TimeZone");
            DropTable("dbo.DeviceNTP");
            DropTable("dbo.DeviceMQ");
            DropTable("dbo.HardwareBase");
            DropTable("dbo.HardwareInApplication");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.ApplicationMQ");
            DropTable("dbo.Application");
            DropTable("dbo.ActuatorType");
        }
    }
}
