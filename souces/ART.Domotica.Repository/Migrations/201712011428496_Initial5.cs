namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial5 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AlterColumn("SI.UnitMeasurementScale", "Name", c => c.String(nullable: false, maxLength: 5, fixedLength: true));
            CreateIndex("SI.UnitMeasurementScale", "Name", unique: true);
        }

        public override void Up()
        {
            DropIndex("SI.UnitMeasurementScale", new[] { "Name" });
            AlterColumn("SI.UnitMeasurementScale", "Name", c => c.String(nullable: false, maxLength: 30));
        }

        #endregion Methods
    }
}