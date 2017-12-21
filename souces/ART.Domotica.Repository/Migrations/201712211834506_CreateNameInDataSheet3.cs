namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNameInDataSheet3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceDatasheet",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DeviceDatasheet", new[] { "Name" });
            DropTable("dbo.DeviceDatasheet");
        }
    }
}
