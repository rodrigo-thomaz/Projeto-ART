namespace ART.Domotica.Repository.Entities
{
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

        #endregion Properties
    }
}