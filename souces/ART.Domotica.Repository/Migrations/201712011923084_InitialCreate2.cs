namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("SI.NumericalScalePrefix", new[] { "Name" });
            AlterColumn("SI.NumericalScalePrefix", "Name", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("SI.NumericalScalePrefix", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("SI.NumericalScalePrefix", new[] { "Name" });
            AlterColumn("SI.NumericalScalePrefix", "Name", c => c.String(nullable: false, maxLength: 5));
            CreateIndex("SI.NumericalScalePrefix", "Name", unique: true);
        }
    }
}
