namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceSensors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublishIntervalInSeconds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceBase", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceSensors", "Id", "dbo.DeviceBase");
            DropIndex("dbo.DeviceSensors", new[] { "Id" });
            DropTable("dbo.DeviceSensors");
        }
    }
}
