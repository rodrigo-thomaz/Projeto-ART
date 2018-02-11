namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate13 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DeviceDatasheet", "HasSensor", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDatasheet", "HasDeviceSensors");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceDatasheet", "HasDeviceSensors", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDatasheet", "HasSensor");
        }

        #endregion Methods
    }
}