namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate6 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddForeignKey("SI.UnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType", "Id");
            AddForeignKey("SI.UnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType", "Id");
            AddForeignKey("SI.UnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix", "Id");
        }

        public override void Up()
        {
            DropForeignKey("SI.UnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("SI.UnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("SI.UnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
        }

        #endregion Methods
    }
}