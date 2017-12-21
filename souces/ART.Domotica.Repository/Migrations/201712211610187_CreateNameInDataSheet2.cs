namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNameInDataSheet2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HardwareInApplication", newName: "DeviceInApplication");
            RenameIndex(table: "dbo.DeviceInApplication", name: "IX_Unique_HardwareId", newName: "IX_Unique_DeviceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DeviceInApplication", name: "IX_Unique_DeviceId", newName: "IX_Unique_HardwareId");
            RenameTable(name: "dbo.DeviceInApplication", newName: "HardwareInApplication");
        }
    }
}
