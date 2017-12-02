namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate5 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.SensorUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
            AddForeignKey("dbo.SensorUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, cascadeDelete: true);
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale");
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
        }

        #endregion Methods
    }
}