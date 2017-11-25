namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Zone", "CountryId", "dbo.Country");
            DropForeignKey("dbo.TimeZone", "ZoneId", "dbo.Zone");
            DropIndex("dbo.Country", new[] { "Code" });
            DropIndex("dbo.Zone", new[] { "CountryId" });
            DropIndex("dbo.TimeZone", new[] { "ZoneId" });
            DropPrimaryKey("dbo.TimeZone");
            AddColumn("dbo.TimeZone", "DisplayName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.TimeZone", "SupportsDaylightSavingTime", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeZone", "UtcTimeOffsetInSecond", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeZone", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.TimeZone", "Id");
            DropColumn("dbo.TimeZone", "ZoneId");
            DropColumn("dbo.TimeZone", "Abreviation");
            DropColumn("dbo.TimeZone", "TimeStart");
            DropColumn("dbo.TimeZone", "GMTOffset");
            DropColumn("dbo.TimeZone", "DST");
            DropTable("dbo.Country");
            DropTable("dbo.Zone");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Zone",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        CountryId = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 35),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TimeZone", "DST", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AddColumn("dbo.TimeZone", "GMTOffset", c => c.Int(nullable: false));
            AddColumn("dbo.TimeZone", "TimeStart", c => c.Decimal(nullable: false, precision: 11, scale: 0));
            AddColumn("dbo.TimeZone", "Abreviation", c => c.String(nullable: false, maxLength: 6));
            AddColumn("dbo.TimeZone", "ZoneId", c => c.Short(nullable: false));
            DropPrimaryKey("dbo.TimeZone");
            AlterColumn("dbo.TimeZone", "Id", c => c.Short(nullable: false, identity: true));
            DropColumn("dbo.TimeZone", "UtcTimeOffsetInSecond");
            DropColumn("dbo.TimeZone", "SupportsDaylightSavingTime");
            DropColumn("dbo.TimeZone", "DisplayName");
            AddPrimaryKey("dbo.TimeZone", "Id");
            CreateIndex("dbo.TimeZone", "ZoneId");
            CreateIndex("dbo.Zone", "CountryId");
            CreateIndex("dbo.Country", "Code", unique: true);
            AddForeignKey("dbo.TimeZone", "ZoneId", "dbo.Zone", "Id");
            AddForeignKey("dbo.Zone", "CountryId", "dbo.Country", "Id");
        }
    }
}
