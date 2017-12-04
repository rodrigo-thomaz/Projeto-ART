namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sensor", "SensorRangeId", "dbo.SensorRange");
            DropIndex("dbo.Sensor", new[] { "SensorRangeId" });
            DropColumn("dbo.Sensor", "SensorRangeId");
            DropTable("dbo.SensorRange");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SensorRange",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Min = c.Short(nullable: false),
                        Max = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sensor", "SensorRangeId", c => c.Byte());
            CreateIndex("dbo.Sensor", "SensorRangeId");
            AddForeignKey("dbo.Sensor", "SensorRangeId", "dbo.SensorRange", "Id");
        }
    }
}
