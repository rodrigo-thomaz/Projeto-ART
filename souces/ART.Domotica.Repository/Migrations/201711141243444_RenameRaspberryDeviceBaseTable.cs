namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameRaspberryDeviceBaseTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RaspberryDeviceBase", newName: "RaspberryDevice");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RaspberryDevice", newName: "RaspberryDeviceBase");
        }
    }
}
