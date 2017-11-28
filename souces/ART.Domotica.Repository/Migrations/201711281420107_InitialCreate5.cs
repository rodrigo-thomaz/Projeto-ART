namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TempSensorRange", newName: "SensorRange");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SensorRange", newName: "TempSensorRange");
        }
    }
}
