namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetHasAlarmRequestContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public bool HasAlarm
        {
            get; set;
        }

        #endregion Properties
    }
}