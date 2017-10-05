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

        #endregion Properties
    }
}