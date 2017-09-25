using System;

namespace ART.Domotica.DistributedServices.Entities
{
    public class DSFamilyTempSensor : SensorBase
    {
        #region Primitive Properties

        public string DeviceAddress { get; set; }
        public string Family { get; set; }
        public Guid TemperatureScaleId { get; set; }

        #endregion

        #region Navigation Properties

        public TemperatureScale TemperatureScale { get; set; }

        #endregion        
    }
}