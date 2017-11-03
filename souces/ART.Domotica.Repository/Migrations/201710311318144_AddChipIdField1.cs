namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddChipIdField1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.ESPDeviceBase", new[] { "ChipId", "FlashChipId", "MacAddress" }, unique: true, name: "IX_Unique_ESPDevice");
        }

        public override void Up()
        {
            DropIndex("dbo.ESPDeviceBase", "IX_Unique_ESPDevice");
        }

        #endregion Methods
    }
}