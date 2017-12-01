namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.NumericalScaleCountry", newSchema: "SI");
        }
        
        public override void Down()
        {
            MoveTable(name: "SI.NumericalScaleCountry", newSchema: "dbo");
        }
    }
}
