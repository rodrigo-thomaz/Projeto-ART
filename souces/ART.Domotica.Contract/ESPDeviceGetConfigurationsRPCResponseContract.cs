namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public Guid? DeviceInApplicationId
        {
            get; set;
        }

        public DeviceMQDetailResponseContract DeviceMQ
        {
            get; set;
        }

        public DeviceNTPDetailResponseContract DeviceNTP
        {
            get; set;
        }

        public int PublishMessageInterval
        {
            get; set;
        }

        #endregion Properties
    }
}