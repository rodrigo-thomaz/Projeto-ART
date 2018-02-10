namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate09 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceSerial", "HasTX");
            DropColumn("dbo.DeviceSerial", "HasRX");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceSerial", "HasRX", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceSerial", "HasTX", c => c.Boolean(nullable: false));
        }

        #endregion Methods
    }
}