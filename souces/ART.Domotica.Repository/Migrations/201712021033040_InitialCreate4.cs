namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate4 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            AddForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType", "Id");
            AddForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement", new[] { "Id", "UnitMeasurementTypeId" });
            AddForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType", "Id");
            AddForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
        }

        #endregion Methods
    }
}