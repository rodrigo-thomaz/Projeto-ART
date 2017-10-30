namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PinChange : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HardwareBase", new[] { "Pin" });
            AddColumn("dbo.ESPDeviceBase", "Pin", c => c.String(nullable: false, maxLength: 4, fixedLength: true));
            CreateIndex("dbo.ESPDeviceBase", "Pin", unique: true);
            DropColumn("dbo.HardwareBase", "Pin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HardwareBase", "Pin", c => c.String(nullable: false, maxLength: 4, fixedLength: true));
            DropIndex("dbo.ESPDeviceBase", new[] { "Pin" });
            DropColumn("dbo.ESPDeviceBase", "Pin");
            CreateIndex("dbo.HardwareBase", "Pin", unique: true);
        }
    }
}
