namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorChartLimiterSetValueRequestContract
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