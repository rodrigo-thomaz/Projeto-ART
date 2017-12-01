namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("SI.UnitMeasurementPrefix", new[] { "Name" });
            AlterColumn("SI.UnitMeasurementPrefix", "Name", c => c.String(nullable: false, maxLength: 5, fixedLength: true));
            CreateIndex("SI.UnitMeasurementPrefix", "Name", unique: true);
        }

        public override void Up()
        {
            DropIndex("SI.UnitMeasurementPrefix", new[] { "Name" });
            AlterColumn("SI.UnitMeasurementPrefix", "Name", c => c.String(nullable: false, maxLength: 5));
            CreateIndex("SI.UnitMeasurementPrefix", "Name", unique: true);
        }

        #endregion Methods
    }
}