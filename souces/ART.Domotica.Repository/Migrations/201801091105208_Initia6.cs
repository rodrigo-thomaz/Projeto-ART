namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initia6 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DeviceSensors", "PublishIntervalInSeconds", c => c.Int(nullable: false));
            DropColumn("dbo.DeviceSensors", "PublishIntervalInMilliSeconds");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceSensors", "PublishIntervalInMilliSeconds", c => c.Int(nullable: false));
            DropColumn("dbo.DeviceSensors", "PublishIntervalInSeconds");
        }

        #endregion Methods
    }
}