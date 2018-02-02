namespace ART.Domotica.IoTContract
{
    using System;

    public class SensorTriggerDeleteResponseIoTContract
    {
        #region Properties

        public Guid SensorId
        {
            get; set;
        }

        public Guid SensorTriggerId
        {
            get; set;
        }

        #endregion Properties
    }
}