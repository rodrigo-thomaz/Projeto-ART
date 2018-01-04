namespace ART.Domotica.IoTContract
{
    using System;

    public class ESPDeviceInsertInApplicationResponseIoTContract
    {
        #region Properties

        public Guid ApplicationId
        {
            get; set;
        }

        public string ApplicationTopic
        {
            get; set;
        }

        #endregion Properties
    }
}