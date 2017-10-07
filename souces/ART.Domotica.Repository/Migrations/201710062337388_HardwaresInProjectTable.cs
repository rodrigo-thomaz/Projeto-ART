namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HardwaresInProjectTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HardwaresInProject",
                c => new
                    {
                        HardwaresInApplicationId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.HardwaresInApplicationId, t.ProjectId })
                .ForeignKey("dbo.HardwaresInApplication", t => t.HardwaresInApplicationId)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .Index(t => t.HardwaresInApplicationId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HardwaresInProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.HardwaresInProject", "HardwaresInApplicationId", "dbo.HardwaresInApplication");
            DropIndex("dbo.HardwaresInProject", new[] { "ProjectId" });
            DropIndex("dbo.HardwaresInProject", new[] { "HardwaresInApplicationId" });
            DropTable("dbo.HardwaresInProject");
        }
    }
}
