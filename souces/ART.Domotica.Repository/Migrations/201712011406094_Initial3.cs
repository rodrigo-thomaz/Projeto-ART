namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial3 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("SI.UnitMeasurementPrefix", "Symbol", unique: true);
        }

        public override void Up()
        {
            DropIndex("SI.UnitMeasurementPrefix", new[] { "Symbol" });
        }

        #endregion Methods
    }
}