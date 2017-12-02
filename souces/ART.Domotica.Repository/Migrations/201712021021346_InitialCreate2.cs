namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale");
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        public override void Up()
        {
            CreateIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
            AddForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.UnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId", "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        #endregion Methods
    }
}