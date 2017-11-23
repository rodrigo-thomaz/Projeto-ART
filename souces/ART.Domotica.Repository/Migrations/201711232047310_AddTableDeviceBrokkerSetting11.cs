namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDeviceBrokkerSetting11 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApplicationMQ", new[] { "User" });
            DropIndex("dbo.ApplicationMQ", new[] { "Password" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ApplicationMQ", "Password", unique: true);
            CreateIndex("dbo.ApplicationMQ", "User", unique: true);
        }
    }
}
