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

        public Guid ApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}