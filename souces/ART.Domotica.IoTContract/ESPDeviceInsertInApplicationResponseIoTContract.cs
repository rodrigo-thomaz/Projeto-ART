namespace ART.Domotica.IoTContract
{
    using System;

    public class ESPDeviceInsertInApplicationResponseIoTContract
    {
        #region Properties

        public string BrokerApplicationTopic
        {
            get; set;
        }

        public Guid DeviceInApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}