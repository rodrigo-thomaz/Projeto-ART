namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveTableThermometerDevice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThermometerDevice", "Id", "dbo.ESPDeviceBase");
            DropIndex("dbo.ThermometerDevice", new[] { "Id" });
            DropTable("dbo.ThermometerDevice");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ThermometerDevice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ThermometerDevice", "Id");
            AddForeignKey("dbo.ThermometerDevice", "Id", "dbo.ESPDeviceBase", "Id");
        }
    }
}
