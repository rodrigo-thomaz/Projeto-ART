namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnSymbol2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TemperatureScale", "Symbol", c => c.String(nullable: false, maxLength: 2, fixedLength: true));
            CreateIndex("dbo.TemperatureScale", "Symbol", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TemperatureScale", new[] { "Symbol" });
            AlterColumn("dbo.TemperatureScale", "Symbol", c => c.String(maxLength: 2, fixedLength: true));
        }
    }
}
