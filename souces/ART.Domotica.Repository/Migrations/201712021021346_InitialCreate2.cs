namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale");
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        public override void Up()
        {
            CreateIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            AddForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        #endregion Methods
    }
}