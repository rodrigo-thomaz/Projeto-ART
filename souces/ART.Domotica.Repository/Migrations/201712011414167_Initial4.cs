namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial4 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("SI.UnitMeasurementScale", "CientificNotation", c => c.Single(nullable: false));
            DropColumn("SI.UnitMeasurementScale", "Exponent");
            DropColumn("SI.UnitMeasurementScale", "Base");
        }

        public override void Up()
        {
            AddColumn("SI.UnitMeasurementScale", "Base", c => c.Short(nullable: false));
            AddColumn("SI.UnitMeasurementScale", "Exponent", c => c.Short(nullable: false));
            DropColumn("SI.UnitMeasurementScale", "CientificNotation");
        }

        #endregion Methods
    }
}