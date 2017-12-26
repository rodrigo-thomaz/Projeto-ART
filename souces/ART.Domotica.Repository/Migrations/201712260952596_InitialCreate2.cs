namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceBinary",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DeviceDatasheetId = c.Short(nullable: false),
                        Binary = c.Binary(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceDatasheet", t => t.DeviceDatasheetId)
                .Index(t => t.DeviceDatasheetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceBinary", "DeviceDatasheetId", "dbo.DeviceDatasheet");
            DropIndex("dbo.DeviceBinary", new[] { "DeviceDatasheetId" });
            DropTable("dbo.DeviceBinary");
        }
    }
}
