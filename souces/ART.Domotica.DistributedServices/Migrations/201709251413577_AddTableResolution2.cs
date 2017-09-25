namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableResolution2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DSFamilyTempSensorResolution", "ConversionTime", c => c.Decimal(nullable: false, precision: 5, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DSFamilyTempSensorResolution", "ConversionTime", c => c.Decimal(nullable: false, precision: 4, scale: 2));
        }
    }
}
