namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public Guid DeviceBaseId
        {
            get; set;
        }

        public Guid? ApplicationId
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