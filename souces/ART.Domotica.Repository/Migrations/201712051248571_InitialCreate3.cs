namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DeviceInApplication", name: "DeviceBaseId", newName: "DeviceId");
            RenameColumn(table: "dbo.HardwaresInProject", name: "DeviceBaseId", newName: "DeviceId");
            RenameIndex(table: "dbo.DeviceInApplication", name: "IX_Unique_DeviceBaseId", newName: "IX_Unique_DeviceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DeviceInApplication", name: "IX_Unique_DeviceId", newName: "IX_Unique_DeviceBaseId");
            RenameColumn(table: "dbo.HardwaresInProject", name: "DeviceId", newName: "DeviceBaseId");
            RenameColumn(table: "dbo.DeviceInApplication", name: "DeviceId", newName: "DeviceBaseId");
        }
    }
}
