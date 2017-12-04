using System;

namespace ART.Domotica.Model
{
    public class SensorsInDeviceDetailModel
    {
        #region Properties

        public Guid DeviceSensorsId
        {
            get; set;
        }
      
        public Guid SensorId
        {
            get; set;
        }

        public short Ordination
        {
            get; set;
        }

        #endregion Properties
    }
}