namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDeviceBrokkerSetting6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceNTPSetting",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeOffset = c.Int(nullable: false),
                        UpdateInterval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceNTPSetting", "Id", "dbo.DeviceBase");
            DropIndex("dbo.DeviceNTPSetting", new[] { "Id" });
            DropTable("dbo.DeviceNTPSetting");
        }
    }
}
