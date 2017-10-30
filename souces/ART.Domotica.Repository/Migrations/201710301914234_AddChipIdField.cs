namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChipIdField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ESPDeviceBase", "ChipId", c => c.String(nullable: false, maxLength: 7, fixedLength: true));
            AddColumn("dbo.ESPDeviceBase", "FlashChipId", c => c.String(nullable: false, maxLength: 7, fixedLength: true));
            CreateIndex("dbo.ESPDeviceBase", "ChipId");
            CreateIndex("dbo.ESPDeviceBase", new[] { "ChipId", "FlashChipId", "MacAddress" }, unique: true, name: "IX_Unique_ESPDevice");
            CreateIndex("dbo.ESPDeviceBase", "FlashChipId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ESPDeviceBase", new[] { "FlashChipId" });
            DropIndex("dbo.ESPDeviceBase", "IX_Unique_ESPDevice");
            DropIndex("dbo.ESPDeviceBase", new[] { "ChipId" });
            DropColumn("dbo.ESPDeviceBase", "FlashChipId");
            DropColumn("dbo.ESPDeviceBase", "ChipId");
        }
    }
}
