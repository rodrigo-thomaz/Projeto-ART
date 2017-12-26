namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceBinary", "Version", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceBinary", "Version");
        }
    }
}
