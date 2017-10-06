namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldApplicationIdInProject3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HardwaresInApplication");
            AlterColumn("dbo.HardwaresInApplication", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.HardwaresInApplication", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.HardwaresInApplication");
            AlterColumn("dbo.HardwaresInApplication", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.HardwaresInApplication", "Id");
        }
    }
}
