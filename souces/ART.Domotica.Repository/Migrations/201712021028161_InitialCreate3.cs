namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate3 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddForeignKey("dbo.SensorUnitMeasurementScale", "SensorTypeId", "dbo.SensorType", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorUnitMeasurementScale", "SensorTypeId", "dbo.SensorType");
        }

        #endregion Methods
    }
}