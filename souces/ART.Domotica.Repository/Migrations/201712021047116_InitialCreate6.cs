namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("SI.UnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix");
            DropForeignKey("SI.UnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType");
            DropForeignKey("SI.UnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
        }
        
        public override void Down()
        {
            AddForeignKey("SI.UnitMeasurementScale", "UnitMeasurementTypeId", "SI.UnitMeasurementType", "Id");
            AddForeignKey("SI.UnitMeasurementScale", "NumericalScaleTypeId", "SI.NumericalScaleType", "Id");
            AddForeignKey("SI.UnitMeasurementScale", "NumericalScalePrefixId", "SI.NumericalScalePrefix", "Id");
        }
    }
}
