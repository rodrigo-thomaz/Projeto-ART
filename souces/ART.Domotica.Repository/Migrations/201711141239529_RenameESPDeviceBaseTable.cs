namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameESPDeviceBaseTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ESPDeviceBase", newName: "ESPDevice");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ESPDevice", newName: "ESPDeviceBase");
        }
    }
}
