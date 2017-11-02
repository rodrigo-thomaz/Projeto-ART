namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChipIdField2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ESPDeviceBase", new[] { "ChipId" });
            DropIndex("dbo.ESPDeviceBase", new[] { "FlashChipId" });
            DropColumn("dbo.ESPDeviceBase", "ChipId");
            DropColumn("dbo.ESPDeviceBase", "FlashChipId");
            AddColumn("dbo.ESPDeviceBase", "ChipId", c => c.Int(nullable: false));
            AddColumn("dbo.ESPDeviceBase", "FlashChipId", c => c.Int(nullable: false));
            CreateIndex("dbo.ESPDeviceBase", "ChipId");
            CreateIndex("dbo.ESPDeviceBase", "FlashChipId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ESPDeviceBase", new[] { "FlashChipId" });
            DropIndex("dbo.ESPDeviceBase", new[] { "ChipId" });
            AlterColumn("dbo.ESPDeviceBase", "FlashChipId", c => c.String(nullable: false, maxLength: 7, fixedLength: true));
            AlterColumn("dbo.ESPDeviceBase", "ChipId", c => c.String(nullable: false, maxLength: 7, fixedLength: true));
            CreateIndex("dbo.ESPDeviceBase", "FlashChipId", unique: true);
            CreateIndex("dbo.ESPDeviceBase", "ChipId");
        }
    }
}
