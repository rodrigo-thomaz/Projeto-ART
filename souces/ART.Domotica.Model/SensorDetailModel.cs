namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorDetailModel
    {
        #region Properties

        public Guid Id
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public SensorChartLimiterDetailModel SensorChartLimiter
        {
            get; set;
        }

        public UnitOfMeasurementEnum UnitOfMeasurementId
        {
            get; set;
        }

        public UnitOfMeasurementTypeEnum UnitOfMeasurementTypeId
        {
            get; protected set;
        }

        #endregion Properties
    }
}