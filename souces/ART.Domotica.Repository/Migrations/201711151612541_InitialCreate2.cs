namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate2 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HasAlarm", c => c.Boolean(nullable: false));
            AlterColumn("dbo.DSFamilyTempSensor", "HighAlarm", c => c.Decimal(precision: 6, scale: 3));
            AlterColumn("dbo.DSFamilyTempSensor", "LowAlarm", c => c.Decimal(precision: 6, scale: 3));
            DropColumn("dbo.DSFamilyTempSensor", "HasHighAlarm");
            DropColumn("dbo.DSFamilyTempSensor", "HasLowAlarm");
        }

        public override void Up()
        {
            AddColumn("dbo.DSFamilyTempSensor", "HasLowAlarm", c => c.Boolean(nullable: false));
            AddColumn("dbo.DSFamilyTempSensor", "HasHighAlarm", c => c.Boolean(nullable: false));
            AlterColumn("dbo.DSFamilyTempSensor", "LowAlarm", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            AlterColumn("dbo.DSFamilyTempSensor", "HighAlarm", c => c.Decimal(nullable: false, precision: 6, scale: 3));
            DropColumn("dbo.DSFamilyTempSensor", "HasAlarm");
        }

        #endregion Methods
    }
}