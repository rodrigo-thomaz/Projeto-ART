namespace ART.Domotica.Model
{
    using System;

    public class SensorsInDeviceGetModel
    {
        #region Properties

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public short Ordination
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        #endregion Properties
    }
}