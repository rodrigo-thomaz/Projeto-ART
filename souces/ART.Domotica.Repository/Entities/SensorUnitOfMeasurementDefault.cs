using ART.Domotica.Enums;
using ART.Infra.CrossCutting.Repository;

namespace ART.Domotica.Repository.Entities
{
    public class SensorUnitOfMeasurementDefault : IEntity<SensorDatasheetEnum>
    {
        public SensorDatasheetEnum Id
        {
            get; set;
        }

        public SensorDatasheet SensorDatasheet
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

        public UnitOfMeasurement UnitOfMeasurement
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
        public decimal Max
        {
            get; set;
        }
        public decimal Min
        {
            get; set;
        }

        
    }
}
