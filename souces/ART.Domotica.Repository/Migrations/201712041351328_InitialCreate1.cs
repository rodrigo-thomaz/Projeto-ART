namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SensorsInDevice", "Order", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SensorsInDevice", "Order");
        }
    }
}
