namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class UnitOfMeasurement : IEntity<byte>
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public ICollection<DSFamilyTempSensor> DSFamilyTempSensors
        {
            get; set;
        }

        public byte Id
        {
            get; set;
        }

        public string Name
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