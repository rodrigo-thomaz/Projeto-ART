namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Create12 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameTable(name: "dbo.SensorInDevice", newName: "SensorsInDevice");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.SensorsInDevice", newName: "SensorInDevice");
        }

        #endregion Methods
    }
}