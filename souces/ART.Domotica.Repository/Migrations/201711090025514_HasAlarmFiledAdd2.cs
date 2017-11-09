namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HasAlarmFiledAdd2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            AlterColumn("dbo.DSFamilyTempSensor", "DeviceAddress", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.DSFamilyTempSensor", "DeviceAddress", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.DSFamilyTempSensor", new[] { "DeviceAddress" });
            AlterColumn("dbo.DSFamilyTempSensor", "DeviceAddress", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.DSFamilyTempSensor", "DeviceAddress", unique: true);
        }
    }
}
