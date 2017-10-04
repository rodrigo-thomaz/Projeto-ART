namespace ART.Data.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BugPlurais : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HardwareInApplication", newName: "HardwaresInApplication");
            RenameTable(name: "dbo.User", newName: "ApplicationUser");
            RenameTable(name: "dbo.UserInApplication", newName: "UsersInApplication");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UsersInApplication", newName: "UserInApplication");
            RenameTable(name: "dbo.ApplicationUser", newName: "User");
            RenameTable(name: "dbo.HardwaresInApplication", newName: "HardwareInApplication");
        }
    }
}
