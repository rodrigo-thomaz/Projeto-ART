namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate3 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.SensorTrigger", "TriggerValue");
        }

        public override void Up()
        {
            AddColumn("dbo.SensorTrigger", "TriggerValue", c => c.String(nullable: false, maxLength: 50));
        }

        #endregion Methods
    }
}