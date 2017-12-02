namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.SensorUnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "SensorTypeId", "dbo.SensorType");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("dbo.SensorUnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("dbo.SensorUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale");
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            DropIndex("dbo.SensorUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" });
            DropTable("dbo.SensorUnitMeasurementScale");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.SensorUnitMeasurementScale",
                c => new
                    {
                        SensorDatasheetId = c.Short(nullable: false),
                        SensorTypeId = c.Short(nullable: false),
                        UnitMeasurementId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.SensorDatasheetId, t.SensorTypeId, t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .ForeignKey("SI.NumericalScale", t => new { t.NumericalScalePrefixId, t.NumericalScaleTypeId }, cascadeDelete: true)
                .ForeignKey("SI.NumericalScalePrefix", t => t.NumericalScalePrefixId)
                .ForeignKey("SI.NumericalScaleType", t => t.NumericalScaleTypeId)
                .ForeignKey("dbo.SensorDatasheet", t => new { t.SensorDatasheetId, t.SensorTypeId })
                .ForeignKey("dbo.SensorType", t => t.SensorTypeId)
                .ForeignKey("SI.UnitMeasurement", t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId })
                .ForeignKey("SI.UnitMeasurementType", t => t.UnitMeasurementTypeId)
                .Index(t => new { t.SensorDatasheetId, t.SensorTypeId })
                .Index(t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId })
                .Index(t => new { t.NumericalScalePrefixId, t.NumericalScaleTypeId });
        }

        #endregion Methods
    }
}