namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using System.Collections.Generic;

    public abstract class SensorBase : HardwareBase
    {
        #region Properties

        public ICollection<SensorsInDevice> SensorsInDevice
        {
            get; set;
        }

        public UnitOfMeasurement UnitOfMeasurement
        {
            get; set;
        }

        public byte UnitOfMeasurementId
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