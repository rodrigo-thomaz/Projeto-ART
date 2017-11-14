namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameIndex(table: "dbo.HardwaresInProject", name: "IX_Unique_DeviceInApplicationId_ProjectId", newName: "IX_Unique_HardwareInApplicationId_ProjectId");
            RenameColumn(table: "dbo.HardwaresInProject", name: "DeviceInApplicationId", newName: "HardwareInApplicationId");
        }

        public override void Up()
        {
            RenameColumn(table: "dbo.HardwaresInProject", name: "HardwareInApplicationId", newName: "DeviceInApplicationId");
            RenameIndex(table: "dbo.HardwaresInProject", name: "IX_Unique_HardwareInApplicationId_ProjectId", newName: "IX_Unique_DeviceInApplicationId_ProjectId");
        }

        #endregion Methods
    }
}