namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial6 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "Label", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.HardwareBase", "Label");
        }

        public override void Up()
        {
            AddColumn("dbo.HardwareBase", "Label", c => c.String(nullable: false, maxLength: 50));
            Sql("UPDATE HardwareBase SET Label = 'Label Teste'");
            DropColumn("dbo.DSFamilyTempSensor", "Label");
        }

        #endregion Methods
    }
}