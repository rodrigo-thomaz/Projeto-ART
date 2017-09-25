namespace ART.Domotica.DistributedServices.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableResolution1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensorResolution", "ResolutionDecimalPlaces", c => c.Byte(nullable: false));
            AlterColumn("dbo.DSFamilyTempSensorResolution", "ConversionTime", c => c.Decimal(nullable: false, precision: 4, scale: 2));
            AlterColumn("dbo.DSFamilyTempSensorResolution", "Resolution", c => c.Decimal(nullable: false, precision: 5, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DSFamilyTempSensorResolution", "Resolution", c => c.Double(nullable: false));
            AlterColumn("dbo.DSFamilyTempSensorResolution", "ConversionTime", c => c.Double(nullable: false));
            DropColumn("dbo.DSFamilyTempSensorResolution", "ResolutionDecimalPlaces");
        }
    }
}
