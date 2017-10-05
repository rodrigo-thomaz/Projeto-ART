namespace ART.Data.Repository.Migrations
{
    using System;
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