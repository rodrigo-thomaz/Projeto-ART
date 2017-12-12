namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Create11 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId", "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "dbo.SensorDatasheetUnitMeasurementScale");
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId", "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            CreateIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            AddForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale");
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            CreateIndex("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId", "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            AddForeignKey("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId", "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "dbo.SensorDatasheetUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId", "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        #endregion Methods
    }
}