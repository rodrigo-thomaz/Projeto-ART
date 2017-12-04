namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.SensorsInDevice", "Order", c => c.Short(nullable: false));
            DropColumn("dbo.SensorsInDevice", "Ordination");
        }

        public override void Up()
        {
            AddColumn("dbo.SensorsInDevice", "Ordination", c => c.Short(nullable: false));
            DropColumn("dbo.SensorsInDevice", "Order");
        }

        #endregion Methods
    }
}