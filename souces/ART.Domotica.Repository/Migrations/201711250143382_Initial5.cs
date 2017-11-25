namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceNTP", "TimeZoneId", c => c.Byte(nullable: false));
            CreateIndex("dbo.DeviceNTP", "TimeZoneId");
            AddForeignKey("dbo.DeviceNTP", "TimeZoneId", "dbo.TimeZone", "Id");
            Sql("UPDATE dbo.DeviceNTP SET TimeZoneId = 1");
            DropColumn("dbo.DeviceNTP", "TimeOffsetInSecond");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceNTP", "TimeOffsetInSecond", c => c.Int(nullable: false));
            DropForeignKey("dbo.DeviceNTP", "TimeZoneId", "dbo.TimeZone");
            DropIndex("dbo.DeviceNTP", new[] { "TimeZoneId" });
            DropColumn("dbo.DeviceNTP", "TimeZoneId");
        }
    }
}
