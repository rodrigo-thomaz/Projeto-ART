namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFieldNameDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "Description", c => c.String());
            AddColumn("dbo.Application", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "Name");
            DropColumn("dbo.Application", "Description");
        }
    }
}
