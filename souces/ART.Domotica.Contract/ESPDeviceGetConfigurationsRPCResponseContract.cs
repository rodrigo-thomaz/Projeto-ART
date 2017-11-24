namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public DeviceMQDetailResponseContract DeviceMQ { get; set; }

        public Guid DeviceId
        {
            get; set;
        }

        public Guid? DeviceInApplicationId
        {
            get; set;
        }

        public string NTPHost
        {
            get; set;
        }

        public int NTPPort
        {
            get; set;
        }

        public int NTPUpdateInterval
        {
            get; set;
        }

        public int PublishMessageInterval
        {
            get; set;
        }

        public int TimeOffset
        {
            get; set;
        }

        #endregion Properties
    }
}