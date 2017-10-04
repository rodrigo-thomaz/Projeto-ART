namespace ART.Data.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableSensorsInDeviceIndex : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            CreateIndex("dbo.SensorsInDevice", "SensorBaseId");
        }

        public override void Up()
        {
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            CreateIndex("dbo.SensorsInDevice", "SensorBaseId", unique: true);
        }

        #endregion Methods
    }
}