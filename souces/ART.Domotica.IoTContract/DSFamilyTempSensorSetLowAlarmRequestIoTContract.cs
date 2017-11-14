namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetLowAlarmRequestIoTContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public decimal LowAlarm
        {
            get; set;
        }

        #endregion Properties
    }
}