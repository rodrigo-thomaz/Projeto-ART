namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate14 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceDatasheet", "HasDeviceSerial");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceDatasheet", "HasDeviceSerial", c => c.Boolean(nullable: false));
        }

        #endregion Methods
    }
}