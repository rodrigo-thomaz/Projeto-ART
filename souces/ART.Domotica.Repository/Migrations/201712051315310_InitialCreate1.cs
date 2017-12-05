namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameIndex(table: "dbo.HardwaresInProject", name: "IX_Unique_HardwareInApplication_ProjectId", newName: "IX_Unique_DeviceInApplication_ProjectId");
            RenameTable(name: "dbo.HardwareInApplication", newName: "DeviceInApplication");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.DeviceInApplication", newName: "HardwareInApplication");
            RenameIndex(table: "dbo.HardwaresInProject", name: "IX_Unique_DeviceInApplication_ProjectId", newName: "IX_Unique_HardwareInApplication_ProjectId");
        }

        #endregion Methods
    }
}