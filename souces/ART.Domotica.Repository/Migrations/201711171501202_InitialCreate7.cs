namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensorResolution", "DecimalPlaces", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DSFamilyTempSensorResolution", "DecimalPlaces");
        }
    }
}
