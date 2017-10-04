namespace ART.Data.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableSensorsInDevice : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.SensorsInDevice", "SensorBaseId", "dbo.SensorBase");
            DropForeignKey("dbo.SensorsInDevice", "DeviceBaseId", "dbo.DeviceBase");
            DropIndex("dbo.SensorsInDevice", new[] { "DeviceBaseId" });
            DropIndex("dbo.SensorsInDevice", new[] { "SensorBaseId" });
            DropTable("dbo.SensorsInDevice");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.SensorsInDevice",
                c => new
                    {
                        SensorBaseId = c.Guid(nullable: false),
                        DeviceBaseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SensorBaseId, t.DeviceBaseId })
                .ForeignKey("dbo.DeviceBase", t => t.DeviceBaseId)
                .ForeignKey("dbo.SensorBase", t => t.SensorBaseId)
                .Index(t => t.SensorBaseId)
                .Index(t => t.DeviceBaseId);
        }

        #endregion Methods
    }
}