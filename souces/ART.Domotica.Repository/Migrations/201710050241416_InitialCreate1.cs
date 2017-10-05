namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateIndex("dbo.Application", "Name", unique: true);
        }

        public override void Up()
        {
            DropIndex("dbo.Application", new[] { "Name" });
        }

        #endregion Methods
    }
}