namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetAlarmOnRequestContract
    {
        #region Properties

        public bool AlarmOn
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public SensorChartLimiterPositionEnum Position
        {
            get; set;
        }

        #endregion Properties
    }
}