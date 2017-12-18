namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial3 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropPrimaryKey("dbo.SensorTrigger");
            AddPrimaryKey("dbo.SensorTrigger", "Id");
        }

        public override void Up()
        {
            DropPrimaryKey("dbo.SensorTrigger");
            AddPrimaryKey("dbo.SensorTrigger", new[] { "Id", "SensorId", "SensorDatasheetId", "SensorTypeId" });
        }

        #endregion Methods
    }
}