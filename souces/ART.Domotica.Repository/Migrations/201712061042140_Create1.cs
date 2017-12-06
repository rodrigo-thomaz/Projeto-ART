namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create1 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.TimeZone", newSchema: "Globalization");
        }
        
        public override void Down()
        {
            MoveTable(name: "Globalization.TimeZone", newSchema: "dbo");
        }
    }
}
