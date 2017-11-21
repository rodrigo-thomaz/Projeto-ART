namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDeviceBrokkerSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceBrokerSetting",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        User = c.String(nullable: false, maxLength: 12),
                        Password = c.String(nullable: false, maxLength: 12),
                        ClientId = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.User, unique: true)
                .Index(t => t.Password, unique: true)
                .Index(t => t.ClientId, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceBrokerSetting", "Id", "dbo.DeviceBase");
            DropIndex("dbo.DeviceBrokerSetting", new[] { "ClientId" });
            DropIndex("dbo.DeviceBrokerSetting", new[] { "Password" });
            DropIndex("dbo.DeviceBrokerSetting", new[] { "User" });
            DropIndex("dbo.DeviceBrokerSetting", new[] { "Id" });
            DropTable("dbo.DeviceBrokerSetting");
        }
    }
}
