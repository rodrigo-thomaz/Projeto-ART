namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTableDeviceBrokkerSetting10 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            DropIndex("dbo.ApplicationMQ", new[] { "Password" });
            DropIndex("dbo.ApplicationMQ", new[] { "User" });
            DropColumn("dbo.ApplicationMQ", "Password");
            DropColumn("dbo.ApplicationMQ", "User");
        }

        public override void Up()
        {
            AddColumn("dbo.ApplicationMQ", "User", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.ApplicationMQ", "Password", c => c.String(nullable: false, maxLength: 12));
            CreateIndex("dbo.ApplicationMQ", "User", unique: true);
            CreateIndex("dbo.ApplicationMQ", "Password", unique: true);
        }

        #endregion Methods
    }
}