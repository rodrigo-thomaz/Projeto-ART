namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceSerial", "BaudRate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceSerial", "BaudRate");
        }
    }
}
