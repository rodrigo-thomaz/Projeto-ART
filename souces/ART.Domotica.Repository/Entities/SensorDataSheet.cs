namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class SensorDatasheet : IEntity<SensorDatasheetEnum>
    {
        #region Properties

        public SensorDatasheetEnum Id
        {
            get; set;
        }

        public ICollection<SensorDatasheetUnitMeasurementScale> SensorDatasheetUnitMeasurementScales
        {
            get; set;
        }

        public SensorType SensorType
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public SensorUnitMeasurementDefault SensorUnitMeasurementDefault
        {
            get; set;
        }

        #endregion Properties
    }
}