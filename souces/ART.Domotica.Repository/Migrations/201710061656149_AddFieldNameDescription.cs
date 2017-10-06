namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFieldNameDescription : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.Application", "Name");
            DropColumn("dbo.Application", "Description");
        }

        public override void Up()
        {
            AddColumn("dbo.Application", "Description", c => c.String());
            AddColumn("dbo.Application", "Name", c => c.String(nullable: false, maxLength: 255));
        }

        #endregion Methods
    }
}