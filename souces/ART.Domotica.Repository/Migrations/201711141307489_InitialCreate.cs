namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.RaspberryDevice", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.ESPDevice", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.DSFamilyTempSensor", "TemperatureScaleId", "dbo.TemperatureScale");
            DropForeignKey("dbo.DSFamilyTempSensor", "DSFamilyTempSensorResolutionId", "dbo.DSFamilyTempSensorResolution");
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwaresInProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Project", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.HardwaresInProject", "HardwareInApplicationId", "dbo.DeviceInApplication");
            DropForeignKey("dbo.HardwaresInProject", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.DeviceInApplication", "DeviceBaseId", "dbo.DeviceBase");
            DropForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.SensorsInDevice", "DeviceBaseId", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceInApplication", "CreateByApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.DeviceInApplication", "ApplicationId", "dbo.Application");
            DropForeignKey("dbo.ApplicationUser", "ApplicationId", "dbo.Application");
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
            DropIndex("dbo.DSFamilyTempSensor", new[] { "TemperatureScaleId" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "Id" });
            DropIndex("dbo.SensorBase", new[] { "Id" });
            DropIndex("dbo.Project", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.Project", new[] { "ApplicationId" });
            DropIndex("dbo.HardwaresInProject", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.HardwaresInProject", "IX_Unique_HardwareInApplicationId_ProjectId");
            DropIndex("dbo.TemperatureScale", new[] { "Symbol" });
            DropIndex("dbo.TemperatureScale", new[] { "Name" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Bits" });
            DropIndex("dbo.DSFamilyTempSensorResolution", new[] { "Name" });
            DropIndex("dbo.SensorsInDevice", new[] { "DeviceBaseId" });
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            DropIndex("dbo.DeviceInApplication", new[] { "CreateByApplicationUserId" });
            DropIndex("dbo.DeviceInApplication", "IX_Unique_DeviceBaseId");
            DropIndex("dbo.DeviceInApplication", "IX_Unique_ApplicationId_DeviceBaseId");
            DropIndex("dbo.ApplicationUser", new[] { "ApplicationId" });
            DropTable("dbo.RaspberryDevice");
            DropTable("dbo.ESPDevice");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.DSFamilyTempSensor");
            DropTable("dbo.SensorBase");
            DropTable("dbo.Project");
            DropTable("dbo.HardwaresInProject");
            DropTable("dbo.TemperatureScale");
            DropTable("dbo.DSFamilyTempSensorResolution");
            DropTable("dbo.SensorsInDevice");
            DropTable("dbo.HardwareBase");
            DropTable("dbo.DeviceInApplication");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Application");
        }

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
                        HardwareInApplicationId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        CreateByApplicationUserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.CreateByApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceInApplication", t => t.HardwareInApplicationId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => new { t.HardwareInApplicationId, t.ProjectId }, unique: true, name: "IX_Unique_HardwareInApplicationId_ProjectId")
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
                        DeviceAddress = c.String(nullable: false, maxLength: 32),
                        Family = c.String(nullable: false, maxLength: 10),
                        HasAlarm = c.Boolean(nullable: false),
                        LowAlarm = c.Decimal(precision: 6, scale: 3),
                        HighAlarm = c.Decimal(precision: 6, scale: 3),
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
                "dbo.ESPDevice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChipId = c.Int(nullable: false),
                        FlashChipId = c.Int(nullable: false),
                        MacAddress = c.String(nullable: false, maxLength: 17, fixedLength: true),
                        Pin = c.String(nullable: false, maxLength: 4, fixedLength: true),
                        TimeOffset = c.Int(nullable: false),
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

        #endregion Methods
    }
}