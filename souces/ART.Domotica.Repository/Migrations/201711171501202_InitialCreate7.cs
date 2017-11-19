namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate7 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DSFamilyTempSensorResolution", "DecimalPlaces");
        }

        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensorResolution", "DecimalPlaces", c => c.Byte(nullable: false));
        }

        #endregion Methods
    }
}