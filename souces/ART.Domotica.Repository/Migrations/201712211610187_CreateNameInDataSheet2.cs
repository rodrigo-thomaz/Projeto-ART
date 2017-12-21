namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateNameInDataSheet2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameIndex(table: "dbo.DeviceInApplication", name: "IX_Unique_DeviceId", newName: "IX_Unique_HardwareId");
            RenameTable(name: "dbo.DeviceInApplication", newName: "HardwareInApplication");
        }

        public override void Up()
        {
            RenameTable(name: "dbo.HardwareInApplication", newName: "DeviceInApplication");
            RenameIndex(table: "dbo.DeviceInApplication", name: "IX_Unique_HardwareId", newName: "IX_Unique_DeviceId");
        }

        #endregion Methods
    }
}