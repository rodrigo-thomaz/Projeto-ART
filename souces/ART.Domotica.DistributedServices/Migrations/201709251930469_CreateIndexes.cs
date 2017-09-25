namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateIndexes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ESPDeviceBase", "MacAddress", c => c.String(nullable: false, maxLength: 17, fixedLength: true));
            AlterColumn("dbo.RaspberryDeviceBase", "LanMacAddress", c => c.String(nullable: false, maxLength: 17, fixedLength: true));
            AlterColumn("dbo.RaspberryDeviceBase", "WLanMacAddress", c => c.String(nullable: false, maxLength: 17, fixedLength: true));
            CreateIndex("dbo.ESPDeviceBase", "MacAddress", unique: true);
            CreateIndex("dbo.RaspberryDeviceBase", "LanMacAddress", unique: true);
            CreateIndex("dbo.RaspberryDeviceBase", "WLanMacAddress", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.RaspberryDeviceBase", new[] { "WLanMacAddress" });
            DropIndex("dbo.RaspberryDeviceBase", new[] { "LanMacAddress" });
            DropIndex("dbo.ESPDeviceBase", new[] { "MacAddress" });
            AlterColumn("dbo.RaspberryDeviceBase", "WLanMacAddress", c => c.String());
            AlterColumn("dbo.RaspberryDeviceBase", "LanMacAddress", c => c.String());
            AlterColumn("dbo.ESPDeviceBase", "MacAddress", c => c.String());
        }
    }
}
