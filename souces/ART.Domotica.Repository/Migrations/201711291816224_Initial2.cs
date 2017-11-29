namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.SensorType", new[] { "Name" });
            DropTable("dbo.SensorType");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.SensorType",
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