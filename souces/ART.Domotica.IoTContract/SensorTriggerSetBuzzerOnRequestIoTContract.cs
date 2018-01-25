namespace ART.Domotica.IoTContract
{
    using System;

    public class SensorTriggerSetBuzzerOnRequestIoTContract
    {
        #region Properties

        public bool BuzzerOn
        {
            get; set;
        }

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