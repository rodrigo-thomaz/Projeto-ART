namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate01 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.RaspberryDevice", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.ESPDevice", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceInApplication", new[] { "DeviceId", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceWiFi", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.SensorInDevice", new[] { "SensorId", "SensorDatasheetId", "SensorTypeId" }, "dbo.Sensor");
            DropForeignKey("dbo.SensorTrigger", new[] { "SensorId", "SensorDatasheetId", "SensorTypeId" }, "dbo.Sensor");
            DropForeignKey("dbo.SensorTempDSFamily", "SensorTempDSFamilyResolutionId", "dbo.SensorTempDSFamilyResolution");
            DropForeignKey("dbo.SensorTempDSFamily", new[] { "Id", "SensorDatasheetId", "SensorTypeId" }, "dbo.Sensor");
            DropForeignKey("dbo.SensorInApplication", new[] { "SensorId", "SensorDatasheetId", "SensorTypeId" }, "dbo.Sensor");
            DropForeignKey("dbo.SensorInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.SensorInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("dbo.SensorDatasheet", "SensorTypeId", "dbo.SensorType");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale");
            DropForeignKey("SI.UnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropForeignKey("SI.UnitMeasurement", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropForeignKey("SI.UnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale");
            DropForeignKey("SI.NumericalScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId", "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "dbo.SensorDatasheetUnitMeasurementScale");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "Id", "SensorDatasheetId", "SensorTypeId" }, "dbo.Sensor");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "NumericalScaleTypeId", "CountryId" }, "SI.NumericalScaleTypeCountry");
            DropForeignKey("SI.NumericalScaleTypeCountry", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("SI.NumericalScaleTypeCountry", "CountryId", "Locale.Country");
            DropForeignKey("Locale.Country", "ContinentId", "Locale.Continent");
            DropForeignKey("SI.NumericalScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "Id", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("dbo.SensorInDevice", new[] { "DeviceSensorsId", "DeviceDatasheetId" }, "dbo.DeviceSensors");
            DropForeignKey("dbo.DeviceSensors", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceNTP", "TimeZoneId", "Globalization.TimeZone");
            DropForeignKey("dbo.DeviceNTP", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceMQ", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceDebug", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceBase", "DeviceDatasheetId", "dbo.DeviceDatasheet");
            DropForeignKey("dbo.DeviceBinary", new[] { "DeviceDatasheetBinaryId", "DeviceDatasheetId" }, "dbo.DeviceDatasheetBinary");
            DropForeignKey("dbo.DeviceDatasheetBinaryBuffer", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceDatasheetBinary");
            DropForeignKey("dbo.DeviceDatasheetBinary", "DeviceDatasheetId", "dbo.DeviceDatasheet");
            DropForeignKey("dbo.DeviceBinary", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.DeviceInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationUser", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationMQ", "Id", "dbo.Application");
            DropIndex("dbo.RaspberryDevice", new[] { "WLanMacAddress" });
            DropIndex("dbo.RaspberryDevice", new[] { "LanMacAddress" });
            DropIndex("dbo.RaspberryDevice", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.ESPDevice", new[] { "Pin" });
            DropIndex("dbo.ESPDevice", new[] { "FlashChipId" });
            DropIndex("dbo.ESPDevice", new[] { "ChipId" });
            DropIndex("dbo.ESPDevice", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceWiFi", new[] { "SoftAPMacAddress" });
            DropIndex("dbo.DeviceWiFi", new[] { "StationMacAddress" });
            DropIndex("dbo.DeviceWiFi", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.SensorTrigger", new[] { "SensorId", "SensorDatasheetId", "SensorTypeId" });
            DropIndex("dbo.SensorTempDSFamilyResolution", new[] { "Bits" });
            DropIndex("dbo.SensorTempDSFamilyResolution", new[] { "Name" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "SensorTempDSFamilyResolutionId" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "DeviceAddress" });
            DropIndex("dbo.SensorTempDSFamily", new[] { "Id", "SensorDatasheetId", "SensorTypeId" });
            DropIndex("dbo.SensorInApplication", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.SensorInApplication", "IX_Unique_SensorInApplication");
            DropIndex("dbo.SensorInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.SensorType", new[] { "Name" });
            DropIndex("SI.UnitMeasurementType", new[] { "Name" });
            DropIndex("SI.UnitMeasurement", new[] { "Symbol" });
            DropIndex("SI.UnitMeasurement", new[] { "Name" });
            DropIndex("SI.UnitMeasurement", new[] { "UnitMeasurementTypeId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" });
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "NumericalScaleTypeId", "CountryId" });
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId", "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "Id", "SensorDatasheetId", "SensorTypeId" });
            DropIndex("Locale.Continent", new[] { "Name" });
            DropIndex("Locale.Country", new[] { "ContinentId" });
            DropIndex("Locale.Country", new[] { "Name" });
            DropIndex("SI.NumericalScaleTypeCountry", new[] { "CountryId" });
            DropIndex("SI.NumericalScaleTypeCountry", new[] { "NumericalScaleTypeId" });
            DropIndex("SI.NumericalScaleType", new[] { "Name" });
            DropIndex("SI.NumericalScalePrefix", new[] { "Name" });
            DropIndex("SI.NumericalScale", new[] { "NumericalScaleTypeId" });
            DropIndex("SI.NumericalScale", new[] { "NumericalScalePrefixId" });
            DropIndex("SI.UnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
            DropIndex("SI.UnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementDefault", new[] { "Id", "SensorTypeId" });
            DropIndex("dbo.SensorDatasheet", new[] { "Name" });
            DropIndex("dbo.SensorDatasheet", new[] { "SensorTypeId" });
            DropIndex("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" });
            DropIndex("dbo.SensorInDevice", "IX_Unique_SensorInDevice");
            DropIndex("dbo.SensorInDevice", new[] { "DeviceSensorsId", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceSensors", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceNTP", new[] { "TimeZoneId" });
            DropIndex("dbo.DeviceNTP", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceMQ", new[] { "Topic" });
            DropIndex("dbo.DeviceMQ", new[] { "ClientId" });
            DropIndex("dbo.DeviceMQ", new[] { "Password" });
            DropIndex("dbo.DeviceMQ", new[] { "User" });
            DropIndex("dbo.DeviceMQ", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceDebug", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceDatasheetBinaryBuffer", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceDatasheet", new[] { "Name" });
            DropIndex("dbo.DeviceDatasheetBinary", new[] { "DeviceDatasheetId" });
            DropIndex("dbo.DeviceBinary", new[] { "DeviceDatasheetBinaryId", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceBinary", new[] { "Id", "DeviceDatasheetId" });
            DropIndex("dbo.DeviceBase", new[] { "DeviceDatasheetId" });
            DropIndex("dbo.DeviceInApplication", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.DeviceInApplication", "IX_Unique_DeviceId");
            DropIndex("dbo.DeviceInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationUser", new[] { "ApplicationId" });
            DropIndex("dbo.ApplicationMQ", new[] { "Topic" });
            DropIndex("dbo.ApplicationMQ", new[] { "Id" });
            DropIndex("dbo.ActuatorType", new[] { "Name" });
            DropTable("dbo.RaspberryDevice");
            DropTable("dbo.ESPDevice");
            DropTable("dbo.DeviceWiFi");
            DropTable("dbo.SensorTrigger");
            DropTable("dbo.SensorTempDSFamilyResolution");
            DropTable("dbo.SensorTempDSFamily");
            DropTable("dbo.SensorInApplication");
            DropTable("dbo.SensorType");
            DropTable("SI.UnitMeasurementType");
            DropTable("SI.UnitMeasurement");
            DropTable("dbo.SensorDatasheetUnitMeasurementScale");
            DropTable("dbo.SensorUnitMeasurementScale");
            DropTable("Locale.Continent");
            DropTable("Locale.Country");
            DropTable("SI.NumericalScaleTypeCountry");
            DropTable("SI.NumericalScaleType");
            DropTable("SI.NumericalScalePrefix");
            DropTable("SI.NumericalScale");
            DropTable("SI.UnitMeasurementScale");
            DropTable("dbo.SensorDatasheetUnitMeasurementDefault");
            DropTable("dbo.SensorDatasheet");
            DropTable("dbo.Sensor");
            DropTable("dbo.SensorInDevice");
            DropTable("dbo.DeviceSensors");
            DropTable("Globalization.TimeZone");
            DropTable("dbo.DeviceNTP");
            DropTable("dbo.DeviceMQ");
            DropTable("dbo.DeviceDebug");
            DropTable("dbo.DeviceDatasheetBinaryBuffer");
            DropTable("dbo.DeviceDatasheet");
            DropTable("dbo.DeviceDatasheetBinary");
            DropTable("dbo.DeviceBinary");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.DeviceInApplication");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.ApplicationMQ");
            DropTable("dbo.Application");
            DropTable("dbo.ActuatorType");
        }

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
                "dbo.DeviceInApplication",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.DeviceId, t.DeviceDatasheetId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId)
                .ForeignKey("dbo.DeviceBase", t => new { t.DeviceId, t.DeviceDatasheetId })
                .Index(t => t.ApplicationId)
                .Index(t => new { t.DeviceId, t.DeviceDatasheetId }, unique: true, name: "IX_Unique_DeviceId")
                .Index(t => t.CreateByApplicationUserId);

            CreateTable(
                "dbo.DeviceBase",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        Label = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceDatasheet", t => t.DeviceDatasheetId)
                .Index(t => t.DeviceDatasheetId);

            CreateTable(
                "dbo.DeviceBinary",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetBinaryId = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceDatasheetBinary", t => new { t.DeviceDatasheetBinaryId, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.DeviceDatasheetBinaryId, t.DeviceDatasheetId });

            CreateTable(
                "dbo.DeviceDatasheetBinary",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        Version = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceDatasheet", t => t.DeviceDatasheetId)
                .Index(t => t.DeviceDatasheetId);

            CreateTable(
                "dbo.DeviceDatasheet",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.DeviceDatasheetBinaryBuffer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        Buffer = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceDatasheetBinary", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId });

            CreateTable(
                "dbo.DeviceDebug",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        RemoteEnabled = c.Boolean(nullable: false),
                        SerialEnabled = c.Boolean(nullable: false),
                        ResetCmdEnabled = c.Boolean(nullable: false),
                        ShowDebugLevel = c.Boolean(nullable: false),
                        ShowTime = c.Boolean(nullable: false),
                        ShowProfiler = c.Boolean(nullable: false),
                        ShowColors = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId });

            CreateTable(
                "dbo.DeviceMQ",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        User = c.String(nullable: false, maxLength: 12),
                        Password = c.String(nullable: false, maxLength: 12),
                        ClientId = c.String(nullable: false, maxLength: 32),
                        Topic = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => t.User, unique: true)
                .Index(t => t.Password, unique: true)
                .Index(t => t.ClientId, unique: true)
                .Index(t => t.Topic, unique: true);

            CreateTable(
                "dbo.DeviceNTP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        TimeZoneId = c.Byte(nullable: false),
                        UpdateIntervalInMilliSecond = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("Globalization.TimeZone", t => t.TimeZoneId)
                .Index(t => new { t.Id, t.DeviceDatasheetId })
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
                        DeviceDatasheetId = c.Guid(nullable: false),
                        ReadIntervalInMilliSeconds = c.Int(nullable: false),
                        PublishIntervalInMilliSeconds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId });

            CreateTable(
                "dbo.SensorInDevice",
                c => new
                    {
                        DeviceSensorsId = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        SensorId = c.Guid(nullable: false),
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        Ordination = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceSensorsId, t.DeviceDatasheetId, t.SensorId, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.DeviceSensors", t => new { t.DeviceSensorsId, t.DeviceDatasheetId })
                .ForeignKey("dbo.Sensor", t => new { t.SensorId, t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => new { t.DeviceSensorsId, t.DeviceDatasheetId })
                .Index(t => new { t.SensorId, t.SensorDatasheetId, t.SensorTypeId }, unique: true, name: "IX_Unique_SensorInDevice");

            CreateTable(
                "dbo.Sensor",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        Label = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.SensorDatasheet", t => new { t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => new { t.SensorDatasheetId, t.SensorTypeId });

            CreateTable(
                "dbo.SensorDatasheet",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorTypeId })
                .ForeignKey("dbo.SensorType", t => t.SensorTypeId)
                .Index(t => t.SensorTypeId)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.SensorDatasheetUnitMeasurementDefault",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        Min = c.Decimal(nullable: false, precision: 9, scale: 4),
                        Max = c.Decimal(nullable: false, precision: 9, scale: 4),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorTypeId })
                .ForeignKey("dbo.SensorDatasheet", t => new { t.Id, t.SensorTypeId })
                .ForeignKey("SI.UnitMeasurementScale", t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .Index(t => new { t.Id, t.SensorTypeId })
                .Index(t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId });

            CreateTable(
                "SI.UnitMeasurementScale",
                c => new
                    {
                        UnitMeasurementId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .ForeignKey("SI.NumericalScale", t => new { t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .ForeignKey("SI.UnitMeasurement", t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId })
                .Index(t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId })
                .Index(t => new { t.NumericalScalePrefixId, t.NumericalScaleTypeId });

            CreateTable(
                "SI.NumericalScale",
                c => new
                    {
                        NumericalScalePrefixId = c.Int(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        ScientificNotationBase = c.Decimal(nullable: false, precision: 24, scale: 12),
                        ScientificNotationExponent = c.Decimal(nullable: false, precision: 24, scale: 12),
                    })
                .PrimaryKey(t => new { t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .ForeignKey("SI.NumericalScalePrefix", t => t.NumericalScalePrefixId)
                .ForeignKey("SI.NumericalScaleType", t => t.NumericalScaleTypeId)
                .Index(t => t.NumericalScalePrefixId)
                .Index(t => t.NumericalScaleTypeId);

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
                "dbo.SensorUnitMeasurementScale",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                        CountryId = c.Short(nullable: false),
                        RangeMax = c.Decimal(nullable: false, precision: 7, scale: 4),
                        RangeMin = c.Decimal(nullable: false, precision: 7, scale: 4),
                        ChartLimiterMax = c.Decimal(nullable: false, precision: 7, scale: 4),
                        ChartLimiterMin = c.Decimal(nullable: false, precision: 7, scale: 4),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("SI.NumericalScaleTypeCountry", t => new { t.NumericalScaleTypeId, t.CountryId })
                .ForeignKey("dbo.Sensor", t => new { t.Id, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.SensorDatasheetUnitMeasurementScale", t => new { t.SensorDatasheetId, t.SensorTypeId, t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .Index(t => new { t.Id, t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => new { t.SensorDatasheetId, t.SensorTypeId, t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .Index(t => new { t.NumericalScaleTypeId, t.CountryId });

            CreateTable(
                "dbo.SensorDatasheetUnitMeasurementScale",
                c => new
                    {
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.SensorDatasheetId, t.SensorTypeId, t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .ForeignKey("dbo.SensorDatasheet", t => new { t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("SI.UnitMeasurementScale", t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .Index(t => new { t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId });

            CreateTable(
                "SI.UnitMeasurement",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Symbol = c.String(nullable: false, maxLength: 5),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.UnitMeasurementTypeId })
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
                "dbo.SensorInApplication",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        SensorId = c.Guid(nullable: false),
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.SensorId, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId)
                .ForeignKey("dbo.Sensor", t => new { t.SensorId, t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => t.ApplicationId)
                .Index(t => new { t.SensorId, t.SensorDatasheetId, t.SensorTypeId }, unique: true, name: "IX_Unique_SensorInApplication")
                .Index(t => t.CreateByApplicationUserId);

            CreateTable(
                "dbo.SensorTempDSFamily",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        DeviceAddress = c.String(nullable: false, maxLength: 32),
                        Family = c.String(nullable: false, maxLength: 10),
                        SensorTempDSFamilyResolutionId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.Sensor", t => new { t.Id, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.SensorTempDSFamilyResolution", t => t.SensorTempDSFamilyResolutionId)
                .Index(t => new { t.Id, t.SensorDatasheetId, t.SensorTypeId })
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
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        TriggerOn = c.Boolean(nullable: false),
                        BuzzerOn = c.Boolean(nullable: false),
                        Max = c.Decimal(nullable: false, precision: 7, scale: 4),
                        Min = c.Decimal(nullable: false, precision: 7, scale: 4),
                    })
                .PrimaryKey(t => new { t.Id, t.SensorId, t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.Sensor", t => new { t.SensorId, t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => new { t.SensorId, t.SensorDatasheetId, t.SensorTypeId });

            CreateTable(
                "dbo.DeviceWiFi",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        StationMacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                        SoftAPMacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                        HostName = c.String(nullable: false, maxLength: 255),
                        PublishIntervalInMilliSeconds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => t.StationMacAddress, unique: true)
                .Index(t => t.SoftAPMacAddress, unique: true);

            CreateTable(
                "dbo.ESPDevice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        ChipId = c.Int(nullable: false),
                        FlashChipId = c.Int(nullable: false),
                        SDKVersion = c.String(nullable: false, maxLength: 50),
                        ChipSize = c.Long(nullable: false),
                        Pin = c.String(nullable: false, maxLength: 4, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => t.ChipId)
                .Index(t => t.FlashChipId, unique: true)
                .Index(t => t.Pin, unique: true);

            CreateTable(
                "dbo.RaspberryDevice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        LanMacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                        WLanMacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => t.LanMacAddress, unique: true)
                .Index(t => t.WLanMacAddress, unique: true);
        }

        #endregion Methods
    }
}