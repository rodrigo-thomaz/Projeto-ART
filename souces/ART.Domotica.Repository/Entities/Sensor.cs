namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public abstract class Sensor : IEntity<Guid>
    {
        #region Properties

        public DateTime CreateDate
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public SensorChartLimiter SensorChartLimiter
        {
            get; set;
        }

        public SensorRange SensorRange
        {
            get; set;
        }

        public byte? SensorRangeId
        {
            get; set;
        }

        public ICollection<SensorsInDevice> SensorsInDevice
        {
            get; set;
        }

        public ICollection<SensorTrigger> SensorTriggers
        {
            get; set;
        }

        public UnitOfMeasurement UnitOfMeasurement
        {
            get; set;
        }

        public UnitOfMeasurementType UnitOfMeasurementType
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