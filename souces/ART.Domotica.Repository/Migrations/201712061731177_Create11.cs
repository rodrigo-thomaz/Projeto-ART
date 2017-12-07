namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Create11 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameTable(name: "dbo.SensorDatasheetUnitMeasurementDefault", newName: "SensorUnitMeasurementDefault");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.SensorUnitMeasurementDefault", newName: "SensorDatasheetUnitMeasurementDefault");
        }

        #endregion Methods
    }
}