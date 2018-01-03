namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initia2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.DeviceDebug", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceBase");
            DropIndex("dbo.DeviceDebug", new[] { "Id", "DeviceDatasheetId" });
            DropTable("dbo.DeviceDebug");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.DeviceDebug",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Short(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceBase", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId });
        }

        #endregion Methods
    }
}