namespace ART.Domotica.DistributedServices.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreateBug2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DSFamilyTempSensor", "Resolution");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "Resolution", c => c.Byte(nullable: false));
        }
    }
}
