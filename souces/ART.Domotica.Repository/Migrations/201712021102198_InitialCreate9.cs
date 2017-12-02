namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate9 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddForeignKey("dbo.Sensor", "UnitMeasurementTypeId", "SI.UnitMeasurementType", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.Sensor", "UnitMeasurementTypeId", "SI.UnitMeasurementType");
        }

        #endregion Methods
    }
}