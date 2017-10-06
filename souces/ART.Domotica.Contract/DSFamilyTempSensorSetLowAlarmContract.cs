namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetLowAlarmContract
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