namespace ART.Domotica.DistributedServices.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableResolution3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DSFamilyTempSensorResolution", "ResolutionDecimalPlaces");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensorResolution", "ResolutionDecimalPlaces", c => c.Byte(nullable: false));
        }
    }
}
