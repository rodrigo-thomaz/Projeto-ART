namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnSymbol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TemperatureScale", "Symbol", c => c.String(maxLength: 2, fixedLength: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TemperatureScale", "Symbol");
        }
    }
}
