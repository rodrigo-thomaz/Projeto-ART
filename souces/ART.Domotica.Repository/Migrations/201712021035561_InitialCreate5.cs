namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate5 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
            AddForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, cascadeDelete: true);
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale");
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        #endregion Methods
    }
}