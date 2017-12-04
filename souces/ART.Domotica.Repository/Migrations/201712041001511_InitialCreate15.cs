namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensor", "SensorDatasheetId", c => c.Short(nullable: false));
            AddColumn("dbo.Sensor", "SensorTypeId", c => c.Short(nullable: false));
            Sql("UPDATE dbo.Sensor SET SensorDatasheetId = 6001, SensorTypeId = 6");
            CreateIndex("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" });
            AddForeignKey("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet", new[] { "Id", "SensorTypeId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropIndex("dbo.Sensor", new[] { "SensorDatasheetId", "SensorTypeId" });
            DropColumn("dbo.Sensor", "SensorTypeId");
            DropColumn("dbo.Sensor", "SensorDatasheetId");
        }
    }
}
