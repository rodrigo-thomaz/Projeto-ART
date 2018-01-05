namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initia3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceDebug", "RemoteEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDebug", "SerialEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDebug", "ResetCmdEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDebug", "ShowDebugLevel", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDebug", "ShowTime", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDebug", "ShowProfiler", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceDebug", "ShowColors", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDebug", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceDebug", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeviceDebug", "ShowColors");
            DropColumn("dbo.DeviceDebug", "ShowProfiler");
            DropColumn("dbo.DeviceDebug", "ShowTime");
            DropColumn("dbo.DeviceDebug", "ShowDebugLevel");
            DropColumn("dbo.DeviceDebug", "ResetCmdEnabled");
            DropColumn("dbo.DeviceDebug", "SerialEnabled");
            DropColumn("dbo.DeviceDebug", "RemoteEnabled");
        }
    }
}
