namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceDatasheet", "HasDeviceSensors", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDatasheet", "HasSensor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceDatasheet", "HasSensor", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDatasheet", "HasDeviceSensors");
        }
    }
}
