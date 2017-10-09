namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.HardwareBase", new[] { "Pin" });
            AlterColumn("dbo.HardwareBase", "Pin", c => c.String(maxLength: 4, fixedLength: true));
        }

        public override void Up()
        {
            AlterColumn("dbo.HardwareBase", "Pin", c => c.String(nullable: false, maxLength: 4, fixedLength: true));
            CreateIndex("dbo.HardwareBase", "Pin", unique: true);
        }

        #endregion Methods
    }
}