namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddChipIdField3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HardwaresInApplication", newName: "HardwareInApplication");
            RenameColumn(table: "dbo.HardwaresInProject", name: "HardwaresInApplicationId", newName: "HardwareInApplicationId");
            RenameIndex(table: "dbo.HardwaresInProject", name: "IX_Unique_HardwaresInApplicationId_ProjectId", newName: "IX_Unique_HardwareInApplicationId_ProjectId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.HardwaresInProject", name: "IX_Unique_HardwareInApplicationId_ProjectId", newName: "IX_Unique_HardwaresInApplicationId_ProjectId");
            RenameColumn(table: "dbo.HardwaresInProject", name: "HardwareInApplicationId", newName: "HardwaresInApplicationId");
            RenameTable(name: "dbo.HardwareInApplication", newName: "HardwaresInApplication");
        }
    }
}
