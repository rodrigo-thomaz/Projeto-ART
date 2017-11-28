namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorChartLimiterSetValueCompletedModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

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