namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorChartLimiterSetValueRequestIoTContract
    {
        #region Properties

        public SensorChartLimiterPositionEnum Position
        {
            get; set;
        }

        public Guid SensorChartLimiterId
        {
            get; set;
        }

        public decimal Value
        {
            get; set;
        }

        #endregion Properties
    }
}