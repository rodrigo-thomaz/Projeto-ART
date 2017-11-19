namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempSensorRange",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TempSensorRange");
        }
    }
}
