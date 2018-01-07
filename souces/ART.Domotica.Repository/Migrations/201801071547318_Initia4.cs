namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initia4 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceDebug", "TelnetTCPPort");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceDebug", "TelnetTCPPort", c => c.Int(nullable: false));
        }

        #endregion Methods
    }
}