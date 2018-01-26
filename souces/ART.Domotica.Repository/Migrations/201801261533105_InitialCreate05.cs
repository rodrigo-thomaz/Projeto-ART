namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate05 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceSensors", "ReadIntervalInMilliSeconds");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceSensors", "ReadIntervalInMilliSeconds", c => c.Int(nullable: false));
        }

        #endregion Methods
    }
}