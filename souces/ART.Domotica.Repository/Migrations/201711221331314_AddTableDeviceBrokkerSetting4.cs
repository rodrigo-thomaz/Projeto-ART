namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting4 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropForeignKey("dbo.ApplicationBrokerSetting", "Id", "dbo.Application");
            DropIndex("dbo.ApplicationBrokerSetting", new[] { "Topic" });
            DropIndex("dbo.ApplicationBrokerSetting", new[] { "Id" });
            DropTable("dbo.ApplicationBrokerSetting");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationBrokerSetting",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Topic = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Application", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Topic, unique: true);
        }

        #endregion Methods
    }
}