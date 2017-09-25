namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TemperatureScale", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TemperatureScale", new[] { "Name" });
        }
    }
}
