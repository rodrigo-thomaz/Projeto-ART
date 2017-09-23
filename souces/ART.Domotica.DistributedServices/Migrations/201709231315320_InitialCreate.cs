namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorBase",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SensorInSpace",
                c => new
                    {
                        SensorId = c.Guid(nullable: false),
                        SpaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SensorId, t.SpaceId })
                .ForeignKey("dbo.SensorBase", t => t.SensorId)
                .ForeignKey("dbo.Space", t => t.SpaceId)
                .Index(t => t.SensorId)
                .Index(t => t.SpaceId);
            
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
                "dbo.DeviceBase",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorInSpace", "SpaceId", "dbo.Space");
            DropForeignKey("dbo.UserInSpace", "UserId", "dbo.User");
            DropForeignKey("dbo.UserInSpace", "SpaceId", "dbo.Space");
            DropForeignKey("dbo.SensorInSpace", "SensorId", "dbo.SensorBase");
            DropIndex("dbo.RaspberryDeviceBase", new[] { "Id" });
            DropIndex("dbo.ThermometerDevice", new[] { "Id" });
            DropIndex("dbo.ESPDeviceBase", new[] { "Id" });
            DropIndex("dbo.DSFamilyTempSensor", new[] { "Id" });
            DropIndex("dbo.UserInSpace", new[] { "SpaceId" });
            DropIndex("dbo.UserInSpace", new[] { "UserId" });
            DropIndex("dbo.SensorInSpace", new[] { "SpaceId" });
            DropIndex("dbo.SensorInSpace", new[] { "SensorId" });
            DropTable("dbo.RaspberryDeviceBase");
            DropTable("dbo.ThermometerDevice");
            DropTable("dbo.ESPDeviceBase");
            DropTable("dbo.DSFamilyTempSensor");
            DropTable("dbo.DeviceBase");
            DropTable("dbo.User");
            DropTable("dbo.UserInSpace");
            DropTable("dbo.Space");
            DropTable("dbo.SensorInSpace");
            DropTable("dbo.SensorBase");
        }
    }
}
