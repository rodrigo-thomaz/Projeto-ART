namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial7 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.TemperatureSensorBase", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.SensorTrigger", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.SensorChartLimiter", "Id", "dbo.SensorBase");
            DropPrimaryKey("dbo.SensorBase");
            AlterColumn("dbo.SensorBase", "Id", c => c.Guid(nullable: false));
            DropColumn("dbo.SensorBase", "CreateDate");
            DropColumn("dbo.SensorBase", "Label");
            AddPrimaryKey("dbo.SensorBase", "Id");
            CreateIndex("dbo.SensorBase", "Id");
            AddForeignKey("dbo.TemperatureSensorBase", "Id", "dbo.SensorBase", "Id");
            AddForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase", "Id");
            AddForeignKey("dbo.SensorTrigger", "SensorBaseId", "dbo.SensorBase", "Id");
            AddForeignKey("dbo.SensorChartLimiter", "Id", "dbo.SensorBase", "Id");
            AddForeignKey("dbo.SensorBase", "Id", "dbo.HardwareBase", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorBase", "Id", "dbo.HardwareBase");
            DropForeignKey("dbo.SensorChartLimiter", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.SensorTrigger", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.TemperatureSensorBase", "Id", "dbo.SensorBase");
            DropIndex("dbo.SensorBase", new[] { "Id" });
            DropPrimaryKey("dbo.SensorBase");
            AddColumn("dbo.SensorBase", "Label", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.SensorBase", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SensorBase", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.SensorBase", "Id");
            AddForeignKey("dbo.SensorChartLimiter", "Id", "dbo.SensorBase", "Id");
            AddForeignKey("dbo.SensorTrigger", "SensorBaseId", "dbo.SensorBase", "Id");
            AddForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase", "Id");
            AddForeignKey("dbo.TemperatureSensorBase", "Id", "dbo.SensorBase", "Id");
        }

        #endregion Methods
    }
}