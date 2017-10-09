namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate1 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropColumn("dbo.HardwareBase", "Pin");
        }

        public override void Up()
        {
            AddColumn("dbo.HardwareBase", "Pin", c => c.String(maxLength: 4, fixedLength: true));
        }

        #endregion Methods
    }
}