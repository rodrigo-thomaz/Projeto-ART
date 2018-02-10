namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate06 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            CreateTable(
                "dbo.SerialInDevice",
                c => new
                    {
                        DeviceSerialId = c.Guid(nullable: false),
                        DeviceId = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        Index = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeviceSerialId, t.DeviceId, t.DeviceDatasheetId });

            DropColumn("dbo.DeviceSerial", "Index");
        }

        public override void Up()
        {
            AddColumn("dbo.DeviceSerial", "Index", c => c.Short(nullable: false));
            DropTable("dbo.SerialInDevice");
        }

        #endregion Methods
    }
}