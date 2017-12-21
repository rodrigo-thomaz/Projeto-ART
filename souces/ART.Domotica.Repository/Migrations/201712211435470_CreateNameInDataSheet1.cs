namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNameInDataSheet1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SensorDatasheet", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.SensorDatasheet", new[] { "Name" });
        }
    }
}
