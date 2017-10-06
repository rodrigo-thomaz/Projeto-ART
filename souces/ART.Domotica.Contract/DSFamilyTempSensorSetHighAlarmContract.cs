namespace ART.Domotica.Contract
{
    using ART.Infra.CrossCutting.MQ;
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