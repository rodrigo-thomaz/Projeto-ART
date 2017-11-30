namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial8 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateTable(
                "dbo.TemperatureSensorBase",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase");
            CreateIndex("dbo.TemperatureSensorBase", "Id");
            AddForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.TemperatureSensorBase", "Id");
            AddForeignKey("dbo.TemperatureSensorBase", "Id", "dbo.SensorBase", "Id");
        }

        public override void Up()
        {
            DropForeignKey("dbo.TemperatureSensorBase", "Id", "dbo.SensorBase");
            DropForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.TemperatureSensorBase");
            DropIndex("dbo.TemperatureSensorBase", new[] { "Id" });
            AddForeignKey("dbo.DSFamilyTempSensor", "Id", "dbo.SensorBase", "Id");
            DropTable("dbo.TemperatureSensorBase");
        }

        #endregion Methods
    }
}