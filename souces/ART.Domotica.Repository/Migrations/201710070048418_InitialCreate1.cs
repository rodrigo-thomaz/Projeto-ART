namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApplicationUser", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.HardwareBase", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "CreateDate");
            DropColumn("dbo.HardwareBase", "CreateDate");
            DropColumn("dbo.ApplicationUser", "CreateDate");
            DropColumn("dbo.Application", "CreateDate");
        }
    }
}
