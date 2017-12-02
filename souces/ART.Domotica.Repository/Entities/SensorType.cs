namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class SensorType : IEntity<SensorTypeEnum>
    {
        #region Properties

        public SensorTypeEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<SensorDatasheet> SensorDatasheets
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

        #endregion Properties
    }
}