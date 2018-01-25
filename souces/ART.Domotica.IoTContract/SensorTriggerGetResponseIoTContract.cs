namespace ART.Domotica.IoTContract
{
    using System;

    public class SensorTriggerGetResponseIoTContract
    {
        #region Properties

        public bool BuzzerOn
        {
            get; set;
        }

        public decimal Max
        {
            get; set;
        }

        public decimal Min
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