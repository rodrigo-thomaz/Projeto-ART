namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.UnitOfMeasurement", newSchema: "SI");
            MoveTable(name: "dbo.UnitOfMeasurementType", newSchema: "SI");
        }
        
        public override void Down()
        {
            MoveTable(name: "SI.UnitOfMeasurementType", newSchema: "dbo");
            MoveTable(name: "SI.UnitOfMeasurement", newSchema: "dbo");
        }
    }
}
