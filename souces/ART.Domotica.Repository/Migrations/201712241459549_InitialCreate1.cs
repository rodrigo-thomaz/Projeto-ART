namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("SI.UnitMeasurementScale", "Name");
        }

        public override void Up()
        {
            AddColumn("SI.UnitMeasurementScale", "Name", c => c.String(nullable: false));
        }

        #endregion Methods
    }
}