namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetHasAlarmRequestIoTContract
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