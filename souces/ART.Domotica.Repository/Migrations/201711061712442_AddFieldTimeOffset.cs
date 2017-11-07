namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFieldTimeOffset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ESPDeviceBase", "TimeOffset", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ESPDeviceBase", "TimeOffset");
        }
    }
}
