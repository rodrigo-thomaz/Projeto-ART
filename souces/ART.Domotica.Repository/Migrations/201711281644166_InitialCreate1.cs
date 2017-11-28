namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorChartLimiter",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Min = c.Decimal(nullable: false, precision: 7, scale: 4),
                        Max = c.Decimal(nullable: false, precision: 7, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensorBase", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorChartLimiter", "Id", "dbo.SensorBase");
            DropIndex("dbo.SensorChartLimiter", new[] { "Id" });
            DropTable("dbo.SensorChartLimiter");
        }
    }
}
