namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SensorsInDevice", "Ordination", c => c.Short(nullable: false));
            DropColumn("dbo.SensorsInDevice", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SensorsInDevice", "Order", c => c.Short(nullable: false));
            DropColumn("dbo.SensorsInDevice", "Ordination");
        }
    }
}
