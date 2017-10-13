namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DeleteTableSetting : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Up()
        {
            DropTable("dbo.Setting");
        }

        #endregion Methods
    }
}