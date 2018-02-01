namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial06 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceWiFi", "PublishIntervalInMilliSeconds");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceWiFi", "PublishIntervalInMilliSeconds", c => c.Int(nullable: false));
        }

        #endregion Methods
    }
}