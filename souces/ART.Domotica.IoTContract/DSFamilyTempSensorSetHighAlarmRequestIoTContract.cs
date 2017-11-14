namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetHighAlarmRequestIoTContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public decimal HighAlarm
        {
            get; set;
        }

        #endregion Properties
    }
}