namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "SensorTypeId", "dbo.SensorType");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" }, "dbo.SensorDatasheet");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("dbo.SensorDatasheetUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale");
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            DropIndex("dbo.SensorDatasheetUnitMeasurementScale", new[] { "SensorDatasheetId", "SensorTypeId" });
            DropTable("dbo.SensorDatasheetUnitMeasurementScale");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.SensorDatasheetUnitMeasurementScale",
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