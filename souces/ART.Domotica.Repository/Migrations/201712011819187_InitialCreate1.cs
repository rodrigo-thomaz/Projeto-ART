namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("SI.UnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
            DropForeignKey("SI.UnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" }, "SI.UnitMeasurement");
            DropForeignKey("SI.UnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("SI.UnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("SI.UnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" }, "SI.NumericalScale");
            DropIndex("SI.UnitMeasurementScale", new[] { "NumericalScalePrefixId", "NumericalScaleTypeId" });
            DropIndex("SI.UnitMeasurementScale", new[] { "UnitMeasurementId", "UnitMeasurementTypeId" });
            DropTable("SI.UnitMeasurementScale");
        }

        public override void Up()
        {
            CreateTable(
                "SI.UnitMeasurementScale",
                c => new
                    {
                        UnitMeasurementId = c.Short(nullable: false),
                        UnitMeasurementTypeId = c.Byte(nullable: false),
                        NumericalScalePrefixId = c.Int(nullable: false),
                        NumericalScaleTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId, t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .ForeignKey("SI.NumericalScale", t => new { t.NumericalScalePrefixId, t.NumericalScaleTypeId })
                .ForeignKey("SI.NumericalScalePrefix", t => t.NumericalScalePrefixId)
                .ForeignKey("SI.NumericalScaleType", t => t.NumericalScaleTypeId)
                .ForeignKey("SI.UnitMeasurement", t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId })
                .ForeignKey("SI.UnitMeasurementType", t => t.UnitMeasurementTypeId)
                .Index(t => new { t.UnitMeasurementId, t.UnitMeasurementTypeId })
                .Index(t => new { t.NumericalScalePrefixId, t.NumericalScaleTypeId });
        }

        #endregion Methods
    }
}