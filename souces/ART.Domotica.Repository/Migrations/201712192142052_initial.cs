namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class initial : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("SI.UnitMeasurement", new[] { "Symbol" });
            AlterColumn("SI.UnitMeasurement", "Symbol", c => c.String(nullable: false, maxLength: 2, fixedLength: true));
            CreateIndex("SI.UnitMeasurement", "Symbol", unique: true);
        }

        public override void Up()
        {
            DropIndex("SI.UnitMeasurement", new[] { "Symbol" });
            AlterColumn("SI.UnitMeasurement", "Symbol", c => c.String(nullable: false, maxLength: 5));
            CreateIndex("SI.UnitMeasurement", "Symbol", unique: true);
        }

        #endregion Methods
    }
}