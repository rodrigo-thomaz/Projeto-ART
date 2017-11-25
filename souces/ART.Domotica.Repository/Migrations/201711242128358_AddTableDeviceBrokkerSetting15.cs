namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeZone", "ZoneId", "dbo.Zone");
            DropPrimaryKey("dbo.Zone");
            AlterColumn("dbo.Zone", "Id", c => c.Short(nullable: false));
            AddPrimaryKey("dbo.Zone", "Id");
            AddForeignKey("dbo.TimeZone", "ZoneId", "dbo.Zone", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeZone", "ZoneId", "dbo.Zone");
            DropPrimaryKey("dbo.Zone");
            AlterColumn("dbo.Zone", "Id", c => c.Short(nullable: false, identity: true));
            AddPrimaryKey("dbo.Zone", "Id");
            AddForeignKey("dbo.TimeZone", "ZoneId", "dbo.Zone", "Id");
        }
    }
}
