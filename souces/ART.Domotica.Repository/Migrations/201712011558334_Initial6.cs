namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Country", new[] { "NumericalScaleId" });
            RenameColumn(table: "dbo.Country", name: "NumericalScaleId", newName: "NumericalScale_Id");
            CreateTable(
                "dbo.NumericalScaleCountry",
                c => new
                    {
                        NumericalScaleId = c.Byte(nullable: false),
                        CountryId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.NumericalScaleId, t.CountryId })
                .ForeignKey("dbo.Country", t => t.CountryId)
                .ForeignKey("SI.NumericalScale", t => t.NumericalScaleId)
                .Index(t => t.NumericalScaleId)
                .Index(t => t.CountryId);
            
            AlterColumn("dbo.Country", "NumericalScale_Id", c => c.Byte());
            CreateIndex("dbo.Country", "NumericalScale_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NumericalScaleCountry", "NumericalScaleId", "SI.NumericalScale");
            DropForeignKey("dbo.NumericalScaleCountry", "CountryId", "dbo.Country");
            DropIndex("dbo.NumericalScaleCountry", new[] { "CountryId" });
            DropIndex("dbo.NumericalScaleCountry", new[] { "NumericalScaleId" });
            DropIndex("dbo.Country", new[] { "NumericalScale_Id" });
            AlterColumn("dbo.Country", "NumericalScale_Id", c => c.Byte(nullable: false));
            DropTable("dbo.NumericalScaleCountry");
            RenameColumn(table: "dbo.Country", name: "NumericalScale_Id", newName: "NumericalScaleId");
            CreateIndex("dbo.Country", "NumericalScaleId");
        }
    }
}
