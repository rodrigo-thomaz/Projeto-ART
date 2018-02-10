namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate04 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropTable("dbo.SerialInDevice");
            DropTable("dbo.DeviceSerial");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.DeviceSerial",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DeviceId = c.Guid(nullable: false),
                        DeviceDatasheetId = c.Guid(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeviceId, t.DeviceDatasheetId });

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
        }

        #endregion Methods
    }
}