namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    // https://pt.wikipedia.org/wiki/Unidade_de_medida
    public class UnitMeasurementType : IEntity<UnitMeasurementTypeEnum>
    {
        #region Properties

        public UnitMeasurementTypeEnum Id
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

        public ICollection<SensorUnitMeasurementScale> SensorUnitMeasurementScales
        {
            get; set;
        }

        public ICollection<UnitMeasurement> UnitMeasurements
        {
            get; set;
        }

        public ICollection<UnitMeasurementScale> UnitMeasurementScales
        {
            get; set;
        }

        #endregion Properties
    }
}