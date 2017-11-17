namespace ART.Domotica.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate5 : DbMigration
    {
        #region Methods

        public override void Down()
        {
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "HighAlarmBuzzerOn", newName: "HighTempSensorAlarmBuzzerOn");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "HighAlarmValue", newName: "HighTempSensorAlarmValue");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "HighAlarmOn", newName: "HighTempSensorAlarmOn");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "LowAlarmBuzzerOn", newName: "LowTempSensorAlarmBuzzerOn");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "LowAlarmValue", newName: "LowTempSensorAlarmValue");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "LowAlarmOn", newName: "LowTempSensorAlarmOn");
        }

        public override void Up()
        {
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "LowTempSensorAlarmOn", newName: "LowAlarmOn");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "LowTempSensorAlarmValue", newName: "LowAlarmValue");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "LowTempSensorAlarmBuzzerOn", newName: "LowAlarmBuzzerOn");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "HighTempSensorAlarmOn", newName: "HighAlarmOn");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "HighTempSensorAlarmValue", newName: "HighAlarmValue");
            RenameColumn(table: "dbo.DSFamilyTempSensor", name: "HighTempSensorAlarmBuzzerOn", newName: "HighAlarmBuzzerOn");
        }

        #endregion Methods
    }
}