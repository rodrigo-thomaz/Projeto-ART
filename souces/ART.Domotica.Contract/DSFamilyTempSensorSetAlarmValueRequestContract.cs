namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetAlarmValueRequestContract
    {
        #region Properties

        public decimal AlarmValue
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
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