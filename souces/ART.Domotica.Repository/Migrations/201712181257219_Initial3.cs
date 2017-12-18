namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SensorTrigger");
            AddPrimaryKey("dbo.SensorTrigger", new[] { "Id", "SensorId", "SensorDatasheetId", "SensorTypeId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SensorTrigger");
            AddPrimaryKey("dbo.SensorTrigger", "Id");
        }
    }
}
