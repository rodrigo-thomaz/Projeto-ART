namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initia5 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DeviceDebug", "TelnetTCPPort", c => c.Int(nullable: false));
        }

        public override void Up()
        {
            DropColumn("dbo.DeviceDebug", "TelnetTCPPort");
        }

        #endregion Methods
    }
}