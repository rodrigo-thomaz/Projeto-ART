namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorTrigger",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SensorBaseId = c.Guid(nullable: false),
                        TriggerOn = c.Boolean(nullable: false),
                        BuzzerOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorBase", t => t.SensorBaseId)
                .Index(t => t.SensorBaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorTrigger", "SensorBaseId", "dbo.SensorBase");
            DropIndex("dbo.SensorTrigger", new[] { "SensorBaseId" });
            DropTable("dbo.SensorTrigger");
        }
    }
}
