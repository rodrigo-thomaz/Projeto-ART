namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate3 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.DeviceBinary", "Version");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceBinary", "Version", c => c.String(nullable: false));
        }

        #endregion Methods
    }
}