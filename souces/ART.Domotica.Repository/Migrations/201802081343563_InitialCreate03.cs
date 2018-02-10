namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate03 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceDatasheet", "HasSensor");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceDatasheet", "HasSensor", c => c.Boolean(nullable: false));
        }

        #endregion Methods
    }
}