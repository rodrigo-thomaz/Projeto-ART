namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate8 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.SensorUnitMeasurementDefault", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            AddForeignKey("dbo.SensorUnitMeasurementDefault", "UnitMeasurementTypeId", "SI.UnitMeasurementType", "Id");
            AddForeignKey("dbo.SensorUnitMeasurementDefault", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement", new[] { "Id", "UnitMeasurementTypeId" });
        }

        public override void Up()
        {
            DropForeignKey("dbo.SensorUnitMeasurementDefault", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropForeignKey("dbo.SensorUnitMeasurementDefault", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropIndex("dbo.SensorUnitMeasurementDefault", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
        }

        #endregion Methods
    }
}