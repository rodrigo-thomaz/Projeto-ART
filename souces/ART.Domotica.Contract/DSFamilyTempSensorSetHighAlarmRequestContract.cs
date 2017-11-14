namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetHighAlarmRequestContract
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