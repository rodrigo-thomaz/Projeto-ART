namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting14 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.TimeZone", "ZoneId", "dbo.Zone");
            DropForeignKey("dbo.Zone", "CountryId", "dbo.Country");
            DropIndex("dbo.TimeZone", new[] { "ZoneId" });
            DropIndex("dbo.Zone", new[] { "CountryId" });
            DropIndex("dbo.Country", new[] { "Code" });
            DropTable("dbo.TimeZone");
            DropTable("dbo.Zone");
            DropTable("dbo.Country");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);

            CreateTable(
                "dbo.Zone",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        CountryId = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.CountryId);

            CreateTable(
                "dbo.TimeZone",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        ZoneId = c.Short(nullable: false),
                        Abreviation = c.String(nullable: false, maxLength: 6),
                        TimeStart = c.Decimal(nullable: false, precision: 11, scale: 0),
                        GMTOffset = c.Int(nullable: false),
                        DST = c.String(nullable: false, maxLength: 1, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zone", t => t.ZoneId)
                .Index(t => t.ZoneId);
        }

        #endregion Methods
    }
}