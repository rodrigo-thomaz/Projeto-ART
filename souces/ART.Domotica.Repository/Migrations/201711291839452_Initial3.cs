namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial3 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.ActuatorType", new[] { "Name" });
            DropTable("dbo.ActuatorType");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.ActuatorType",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
        }

        #endregion Methods
    }
}