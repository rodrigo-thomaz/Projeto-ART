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

        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        public UnitMeasurementTypeEnum UnitMeasurementTypeId
        {
            get; protected set;
        }

        #endregion Properties
    }
}