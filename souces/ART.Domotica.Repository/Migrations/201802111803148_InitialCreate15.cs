namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceSerial", "SerialMode", c => c.Byte(nullable: false));
            DropColumn("dbo.DeviceSerial", "HasRX");
            DropColumn("dbo.DeviceSerial", "HasTX");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceSerial", "HasTX", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceSerial", "HasRX", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceSerial", "SerialMode");
        }
    }
}
