namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate17 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.DeviceType", new[] { "Name" });
            DropTable("dbo.DeviceType");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.DeviceType",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
        }

        #endregion Methods
    }
}