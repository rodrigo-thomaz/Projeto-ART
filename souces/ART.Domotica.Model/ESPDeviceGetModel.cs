namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceGetModel
    {
        #region Properties         

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceNTPGetModel DeviceNTP
        {
            get; set;
        }

        public DeviceSensorsGetModel DeviceSensors
        {
            get; set;
        }       

        public string Label
        {
            get; set;
        }

        #endregion Properties
    }
}