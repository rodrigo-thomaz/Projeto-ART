namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate16 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceSerial", "BaudRate");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceSerial", "BaudRate", c => c.Int(nullable: false));
        }

        #endregion Methods
    }
}