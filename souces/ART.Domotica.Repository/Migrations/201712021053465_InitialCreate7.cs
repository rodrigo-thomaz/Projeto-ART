namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate7 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.SensorUnitMeasurementDefault", "NumericalScaleTypeId");
            CreateIndex("dbo.SensorUnitMeasurementDefault", "NumericalScalePrefixId");
            AddForeignKey("dbo.SensorUnitMeasurementDefault", "SensorTypeId", "dbo.SensorType", "Id");
            AddForeignKey("dbo.SensorUnitMeasurementDefault", "NumericalScaleTypeId", "SI.NumericalScaleType", "Id");
            AddForeignKey("dbo.SensorUnitMeasurementDefault", "NumericalScalePrefixId", "SI.NumericalScalePrefix", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorUnitMeasurementDefault", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("dbo.SensorUnitMeasurementDefault", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("dbo.SensorUnitMeasurementDefault", "SensorTypeId", "dbo.SensorType");
            DropIndex("dbo.SensorUnitMeasurementDefault", new[] { "NumericalScalePrefixId" });
            DropIndex("dbo.SensorUnitMeasurementDefault", new[] { "NumericalScaleTypeId" });
        }

        #endregion Methods
    }
}