namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceSerial", "HasRX", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceSerial", "HasTX", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceSerial", "HasTX");
            DropColumn("dbo.DeviceSerial", "HasRX");
        }
    }
}
