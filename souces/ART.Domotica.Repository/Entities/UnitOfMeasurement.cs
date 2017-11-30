namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;

    public class UnitOfMeasurement
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public UnitOfMeasurementEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<Sensor> Sensors
        {
            get; set;
        }

        public ICollection<SensorUnitOfMeasurementDefault> SensorUnitOfMeasurementDefaults
        {
            get; set;
        }

        public string Symbol
        {
            get; set;
        }

        public UnitOfMeasurementType UnitOfMeasurementType
        {
            get; set;
        }

        public UnitOfMeasurementTypeEnum UnitOfMeasurementTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}