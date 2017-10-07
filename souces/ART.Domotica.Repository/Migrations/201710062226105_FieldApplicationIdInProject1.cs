namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FieldApplicationIdInProject1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Application", "Description");
            DropColumn("dbo.Application", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Application", "Name", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Application", "Description", c => c.String());
        }
    }
}
