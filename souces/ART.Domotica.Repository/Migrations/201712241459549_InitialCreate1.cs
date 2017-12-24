namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("SI.UnitMeasurementScale", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("SI.UnitMeasurementScale", "Name");
        }
    }
}
