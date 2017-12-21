namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateNameInDataSheet : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.SensorDatasheet", "Name");
        }

        public override void Up()
        {
            AddColumn("dbo.SensorDatasheet", "Name", c => c.String(nullable: false, maxLength: 50));
        }

        #endregion Methods
    }
}