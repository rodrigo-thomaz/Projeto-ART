namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public Guid? ApplicationId
        {
            get; set;
        }

        public Guid DeviceId
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