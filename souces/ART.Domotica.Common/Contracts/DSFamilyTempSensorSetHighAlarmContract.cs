namespace ART.Domotica.Common.Contracts
{
    using System;

    public class DSFamilyTempSensorSetHighAlarmContract
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