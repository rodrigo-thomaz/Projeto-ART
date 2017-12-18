namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SensorTrigger", "Max", c => c.Decimal(nullable: false, precision: 7, scale: 4));
            AddColumn("dbo.SensorTrigger", "Min", c => c.Decimal(nullable: false, precision: 7, scale: 4));
            DropColumn("dbo.SensorTrigger", "TriggerValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SensorTrigger", "TriggerValue", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.SensorTrigger", "Min");
            DropColumn("dbo.SensorTrigger", "Max");
        }
    }
}
