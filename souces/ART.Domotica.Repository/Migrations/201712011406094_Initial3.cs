namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("SI.UnitMeasurementPrefix", new[] { "Symbol" });
        }
        
        public override void Down()
        {
            CreateIndex("SI.UnitMeasurementPrefix", "Symbol", unique: true);
        }
    }
}
