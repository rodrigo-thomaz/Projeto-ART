namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Sensor", "UnitOfMeasurementTypeId", "dbo.UnitOfMeasurementType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensor", "UnitOfMeasurementTypeId", "dbo.UnitOfMeasurementType");
        }
    }
}
