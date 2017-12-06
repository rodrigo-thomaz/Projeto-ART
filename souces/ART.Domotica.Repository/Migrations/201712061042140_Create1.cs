namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Create1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            MoveTable(name: "Globalization.TimeZone", newSchema: "dbo");
        }

        public override void Up()
        {
            MoveTable(name: "dbo.TimeZone", newSchema: "Globalization");
        }

        #endregion Methods
    }
}