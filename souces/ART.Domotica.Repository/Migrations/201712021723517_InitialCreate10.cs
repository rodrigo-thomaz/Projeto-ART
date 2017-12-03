namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate10 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Country", newSchema: "Locale");
            MoveTable(name: "dbo.Continent", newSchema: "Locale");
        }
        
        public override void Down()
        {
            MoveTable(name: "Locale.Continent", newSchema: "dbo");
            MoveTable(name: "Locale.Country", newSchema: "dbo");
        }
    }
}
