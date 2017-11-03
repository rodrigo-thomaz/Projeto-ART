namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetConfigurationsResponseContract
    {
        #region Properties

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

        public Guid HardwareId
        {
            get; set;
        }

        public Guid? HardwareInApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}