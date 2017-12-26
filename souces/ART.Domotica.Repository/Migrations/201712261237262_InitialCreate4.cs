namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DeviceBinary", newName: "DeviceDatasheetBinary");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DeviceDatasheetBinary", newName: "DeviceBinary");
        }
    }
}
