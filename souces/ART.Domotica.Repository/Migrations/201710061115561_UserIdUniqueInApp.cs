namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserIdUniqueInApp : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.UsersInApplication", new[] { "UserId" });
            CreateIndex("dbo.UsersInApplication", "UserId");
        }

        public override void Up()
        {
            DropIndex("dbo.UsersInApplication", new[] { "UserId" });
            CreateIndex("dbo.UsersInApplication", "UserId", unique: true);
        }

        #endregion Methods
    }
}