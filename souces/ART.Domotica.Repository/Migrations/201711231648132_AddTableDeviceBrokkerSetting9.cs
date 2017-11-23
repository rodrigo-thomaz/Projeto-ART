namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDeviceBrokkerSetting9 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationBrokerSetting", newName: "ApplicationMQ");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ApplicationMQ", newName: "ApplicationBrokerSetting");
        }
    }
}
