namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate12 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameTable(name: "dbo.SensorDatasheetUnitMeasurementScale", newName: "SensorUnitMeasurementScale");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.SensorUnitMeasurementScale", newName: "SensorDatasheetUnitMeasurementScale");
        }

        #endregion Methods
    }
}