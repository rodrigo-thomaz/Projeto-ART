namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class UnitMeasurement : IEntity<UnitMeasurementEnum>
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public UnitMeasurementEnum Id
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

        public ICollection<SensorUnitMeasurementDefault> SensorUnitMeasurementDefaults
        {
            get; set;
        }

        public string Symbol
        {
            get; set;
        }

        public UnitMeasurementType UnitMeasurementType
        {
            get; set;
        }

        public UnitMeasurementTypeEnum UnitMeasurementTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}