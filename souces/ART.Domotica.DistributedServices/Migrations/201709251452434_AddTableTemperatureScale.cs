namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTemperatureScale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TemperatureScale",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TemperatureScale");
        }
    }
}
