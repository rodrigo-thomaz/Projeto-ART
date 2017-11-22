namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public string BrokerApplicationTopic
        {
            get; set;
        }

        public string BrokerClientId
        {
            get; set;
        }

        public string BrokerDeviceTopic
        {
            get; set;
        }

        public string BrokerHost
        {
            get; set;
        }

        public string BrokerPassword
        {
            get; set;
        }

        public int BrokerPort
        {
            get; set;
        }

        public string BrokerUser
        {
            get; set;
        }

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