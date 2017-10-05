namespace ART.Domotica.Common.Contracts
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