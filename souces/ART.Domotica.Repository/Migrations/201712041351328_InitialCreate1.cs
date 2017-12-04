namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.SensorsInDevice", "Order");
        }

        public override void Up()
        {
            AddColumn("dbo.SensorsInDevice", "Order", c => c.Short(nullable: false));
        }

        #endregion Methods
    }
}