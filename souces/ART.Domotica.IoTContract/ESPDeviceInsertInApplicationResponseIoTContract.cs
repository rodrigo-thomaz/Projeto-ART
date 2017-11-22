namespace ART.Domotica.IoTContract
{
    using System;

    public class ESPDeviceInsertInApplicationResponseIoTContract
    {
        #region Properties

        public Guid DeviceInApplicationId
        {
            get; set;
        }

        public string BrokerApplicationTopic
        {
            get; set;
        }

        #endregion Properties
    }
}