namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetAlarmValueRequestContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public decimal AlarmValue
        {
            get; set;
        }

        public TempSensorAlarmPositionContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}