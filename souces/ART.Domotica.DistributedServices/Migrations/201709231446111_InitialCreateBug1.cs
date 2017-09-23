namespace ART.Domotica.DistributedServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateBug1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HardwareInSpace", "SensorBase_Id", "dbo.SensorBase");
            DropIndex("dbo.HardwareInSpace", new[] { "SensorBase_Id" });
            DropColumn("dbo.HardwareInSpace", "SensorBase_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HardwareInSpace", "SensorBase_Id", c => c.Guid());
            CreateIndex("dbo.HardwareInSpace", "SensorBase_Id");
            AddForeignKey("dbo.HardwareInSpace", "SensorBase_Id", "dbo.SensorBase", "Id");
        }
    }
}
