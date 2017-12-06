namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create11 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SensorUnitMeasurementDefault", newName: "SensorDatasheetUnitMeasurementDefault");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SensorDatasheetUnitMeasurementDefault", newName: "SensorUnitMeasurementDefault");
        }
    }
}
