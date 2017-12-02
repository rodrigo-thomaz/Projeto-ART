namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate4 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            AddForeignKey("dbo.SensorUnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType", "Id");
            AddForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement", new[] { "Id", "UnitMeasurementTypeId" });
            AddForeignKey("dbo.SensorUnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType", "Id");
            AddForeignKey("dbo.SensorUnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorUnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
        }

        #endregion Methods
    }
}