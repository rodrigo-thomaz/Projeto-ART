namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DeviceDatasheetBinary");
            AddPrimaryKey("dbo.DeviceDatasheetBinary", new[] { "Id", "DeviceDatasheetId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DeviceDatasheetBinary");
            AddPrimaryKey("dbo.DeviceDatasheetBinary", "Id");
        }
    }
}
