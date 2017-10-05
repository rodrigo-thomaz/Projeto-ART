namespace ART.Data.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        #region Methods

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
            DropForeignKey("dbo.UsersInApplication", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.UsersInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.HardwaresInApplication", "HardwareBaseId", "dbo.HardwareBase");
            DropForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.SensorsInDevice", "DeviceBaseId", "dbo.DeviceBase");
            DropForeignKey("dbo.HardwaresInApplication", "ApplicationId", "dbo.Application");
            DropIndex("dbo.RaspberryDeviceBase", new[] { "WLanMacAddress" });
            DropIndex("dbo.RaspberryDeviceBase", new[] { "LanMacAddress" });
            DropIndex("dbo.RaspberryDeviceBase", new[] { "Id" });
            DropIndex("dbo.ThermometerDevice", new[] { "Id" });
            DropIndex("dbo.ESPDeviceBase", new[] { "MacAddress" });
            DropIndex("dbo.ESPDeviceBase", new[] { "Id" });
            DropIndex("dbo.DeviceBase", new[] { "Id" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "TemperatureScaleId" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DSFamilyTempSensorResolutionId" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "Id" });
            DropIndex("dbo.SensorBase", new[] { "Id" });
            DropIndex("dbo.UsersInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.UsersInApplication", new[] { "UserId" });
            DropIndex("dbo.TemperatureScale", new[] { "Symbol" });
            DropIndex("dbo.TemperatureScale", new[] { "Name" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Name" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Bits" });
            DropIndex("dbo.SensorsInDevice", new[] { "DeviceBaseId" });
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            DropIndex("dbo.HardwaresInApplication", new[] { "ApplicationId" });
            DropIndex("dbo.HardwaresInApplication", new[] { "HardwareBaseId" });
            DropIndex("dbo.Application", new[] { "Name" });
            DropTable("dbo.RaspberryDeviceBase");
            DropTable("dbo.ThermometerDevice");
            DropTable("dbo.ESPDeviceBase");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.DSFamilyTempSensor");
            DropTable("dbo.SensorBase");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.UsersInApplication");
            DropTable("dbo.TemperatureScale");
            DropTable("dbo.DSFamilyTempSensorResolution");
            DropTable("dbo.SensorsInDevice");
            DropTable("dbo.HardwareBase");
            DropTable("dbo.HardwaresInApplication");
            DropTable("dbo.Application");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.HardwaresInApplication",
                c => new
                    {
                        HardwareBaseId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.HardwareBaseId, t.ApplicationId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.HardwareBase", t => t.HardwareBaseId)
                .Index(t => t.HardwareBaseId)
                .Index(t => t.ApplicationId);

            CreateTable(
                "dbo.HardwareBase",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
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
                        Bits = c.Byte(nullable: false),
                        ConversionTime = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 255),
                        Resolution = c.Decimal(nullable: false, precision: 5, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Bits, unique: true)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.TemperatureScale",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 255),
                        Symbol = c.String(nullable: false, maxLength: 2, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Symbol, unique: true);

            CreateTable(
                "dbo.UsersInApplication",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ApplicationId })
                .ForeignKey("dbo.Application", t => t.ApplicationId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);

            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

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
                        DSFamilyTempSensorResolutionId = c.Byte(nullable: false),
                        Family = c.String(nullable: false, maxLength: 10),
                        HighAlarm = c.Decimal(nullable: false, precision: 6, scale: 3),
                        LowAlarm = c.Decimal(nullable: false, precision: 6, scale: 3),
                        TemperatureScaleId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorBase", t => t.Id)
                .ForeignKey("dbo.DSFamilyTempSensorResolution", t => t.DSFamilyTempSensorResolutionId)
                .ForeignKey("dbo.TemperatureScale", t => t.TemperatureScaleId)
                .Index(t => t.Id)
                .Index(t => t.DeviceAddress, unique: true)
                .Index(t => t.DSFamilyTempSensorResolutionId)
                .Index(t => t.TemperatureScaleId);

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

        #endregion Methods
    }
}