namespace ART.Domotica.IoTContract
{
    using System;

    public class SensorTriggerSetTriggerOnRequestIoTContract
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

        public bool TriggerOn
        {
            get; set;
        }

        #endregion Properties
    }
}