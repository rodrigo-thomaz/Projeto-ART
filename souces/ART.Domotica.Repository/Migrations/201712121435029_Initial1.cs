namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sensor", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "Id", "dbo.Sensor");
            DropForeignKey("dbo.SensorInApplication", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorTempDSFamily", "Id", "dbo.Sensor");
            DropForeignKey("dbo.SensorTrigger", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorInDevice", "SensorId", "dbo.Sensor");
            DropIndex("dbo.Sensor", new[] { "Id" });
            DropPrimaryKey("dbo.Sensor");
            AddColumn("dbo.Sensor", "Label", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Sensor", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Sensor", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorUnitMeasurementScale", "Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorInApplication", "SensorId", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorTempDSFamily", "Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorTrigger", "SensorId", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorInDevice", "SensorId", "dbo.Sensor", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorInDevice", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorTrigger", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorTempDSFamily", "Id", "dbo.Sensor");
            DropForeignKey("dbo.SensorInApplication", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "Id", "dbo.Sensor");
            DropPrimaryKey("dbo.Sensor");
            AlterColumn("dbo.Sensor", "Id", c => c.Guid(nullable: false));
            DropColumn("dbo.Sensor", "CreateDate");
            DropColumn("dbo.Sensor", "Label");
            AddPrimaryKey("dbo.Sensor", "Id");
            CreateIndex("dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorInDevice", "SensorId", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorTrigger", "SensorId", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorTempDSFamily", "Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorInApplication", "SensorId", "dbo.Sensor", "Id");
            AddForeignKey("dbo.SensorUnitMeasurementScale", "Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.Sensor", "Id", "dbo.HardwareBase", "Id");
        }
    }
}
