namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DeviceDatasheetBinary", "Binary", c => c.Binary(nullable: false));
            DropForeignKey("dbo.DeviceDatasheetBinaryBuffer", new[] { "Id", "DeviceDatasheetId" }, "dbo.DeviceDatasheetBinary");
            DropIndex("dbo.DeviceDatasheetBinaryBuffer", new[] { "Id", "DeviceDatasheetId" });
            DropTable("dbo.DeviceDatasheetBinaryBuffer");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.DeviceDatasheetBinaryBuffer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Short(nullable: false),
                        Buffer = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceDatasheetId })
                .ForeignKey("dbo.DeviceDatasheetBinary", t => new { t.Id, t.DeviceDatasheetId })
                .Index(t => new { t.Id, t.DeviceDatasheetId });

            DropColumn("dbo.DeviceDatasheetBinary", "Binary");
        }

        #endregion Methods
    }
}