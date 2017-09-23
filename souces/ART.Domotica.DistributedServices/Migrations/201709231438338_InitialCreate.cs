namespace ART.Domotica.DistributedServices.Migrations
{
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
                "dbo.HardwareInSpace",
                c => new
                    {
                        HardwareBaseId = c.Guid(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                        SensorBase_Id = c.Guid(),
                    })
                .PrimaryKey(t => new { t.HardwareBaseId, t.SpaceId })
                .ForeignKey("dbo.SensorBase", t => t.SensorBase_Id)
                .ForeignKey("dbo.HardwareBase", t => t.HardwareBaseId)
                .ForeignKey("dbo.Space", t => t.SpaceId)
                .Index(t => t.HardwareBaseId)
                .Index(t => t.SpaceId)
                .Index(t => t.SensorBase_Id);
            
            CreateTable(
                "dbo.Space",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        DeviceAddress = c.String(),
                        Family = c.String(),
                        Resolution = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorBase", t => t.Id)
                .Index(t => t.Id);
            
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
                        MacAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id);
            
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
                        LanMacAddress = c.String(),
                        WLanMacAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaspberryDeviceBase", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.ThermometerDevice", "Id", "dbo.ESPDeviceBase");
            DropForeignKey("dbo.ESPDeviceBase", "Id", "dbo.DeviceBase");
            DropForeignKey("dbo.DeviceBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwareInSpace", "SpaceId", "dbo.Space");
            DropForeignKey("dbo.UserInSpace", "UserId", "dbo.User");
            DropForeignKey("dbo.UserInSpace", "SpaceId", "dbo.Space");
            DropForeignKey("dbo.HardwareInSpace", "HardwareBaseId", "dbo.HardwareBase");
            DropForeignKey("dbo.HardwareInSpace", "SensorBase_Id", "dbo.SensorBase");
            DropIndex("dbo.RaspberryDeviceBase", new[] { "Id" });
            DropIndex("dbo.ThermometerDevice", new[] { "Id" });
            DropIndex("dbo.ESPDeviceBase", new[] { "Id" });
            DropIndex("dbo.DeviceBase", new[] { "Id" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "Id" });
            DropIndex("dbo.SensorBase", new[] { "Id" });
            DropIndex("dbo.UserInSpace", new[] { "SpaceId" });
            DropIndex("dbo.UserInSpace", new[] { "UserId" });
            DropIndex("dbo.HardwareInSpace", new[] { "SensorBase_Id" });
            DropIndex("dbo.HardwareInSpace", new[] { "SpaceId" });
            DropIndex("dbo.HardwareInSpace", new[] { "HardwareBaseId" });
            DropTable("dbo.RaspberryDeviceBase");
            DropTable("dbo.ThermometerDevice");
            DropTable("dbo.ESPDeviceBase");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.DSFamilyTempSensor");
            DropTable("dbo.SensorBase");
            DropTable("dbo.User");
            DropTable("dbo.UserInSpace");
            DropTable("dbo.Space");
            DropTable("dbo.HardwareInSpace");
            DropTable("dbo.HardwareBase");
        }
    }
}
